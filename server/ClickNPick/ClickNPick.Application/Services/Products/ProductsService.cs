using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.DtoModels;
using ClickNPick.Application.DtoModels.Products.Request;
using ClickNPick.Application.DtoModels.Products.Response;
using ClickNPick.Application.Exceptions.Categories;
using ClickNPick.Application.Exceptions.Identity;
using ClickNPick.Application.Exceptions.Products;
using ClickNPick.Application.Exceptions.PromotionPricings;
using ClickNPick.Application.Services.Categories;
using ClickNPick.Application.Services.Images;
using ClickNPick.Application.Services.Payment;
using ClickNPick.Application.Services.PromotionPricings;
using ClickNPick.Application.Services.Users;
using ClickNPick.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product = ClickNPick.Domain.Models.Product;


namespace ClickNPick.Application.Services.Products;

public class ProductsService : IProductsService
{
    private const int ThumbnailWidth = 288;
    private const int ThumbnailHeight = 320;
    private const int SliderWidth = 1024;
    private const int SliderHeight = 1024;

    private readonly IRepository<Product> _productsRepository;
    private readonly IUsersService _usersService;
    private readonly IImagesService _imagesService;
    private readonly IPromotionPricingService _promotionPricingService;
    private readonly ICategoriesService _categoriesService;

    public ProductsService(
        IRepository<Product> productsRepository,
        IUsersService usersService,
        IImagesService imagesService,
        IPaymentService stripeService,
        IPromotionPricingService promotionPricingService,
        ICategoriesService categoriesService)
    {
        _productsRepository = productsRepository;
        _usersService = usersService;
        _imagesService = imagesService;
        _promotionPricingService = promotionPricingService;
        _categoriesService = categoriesService;
    }

    public async Task<string> CreateProductAsync(CreateProductRequestDto model)
    {
        var user = await _usersService.GetByIdAsync(model.CreatorId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var category = await _categoriesService.GetByIdAsync(model.CategoryId);

        if (category == null)
        {
            throw new CategoryNotFoundException("Category with id not found.");
        }

        if (model.Images.Any() == false)
        {
            throw new ArgumentException("Atleast one image is required to create a product.");
        }

        if (model.ThumbnailImage == null)
        {
            throw new ArgumentException("Thumbnail image required to create a product");
        }

        var newProduct = model.ToProduct();

        var images = new List<Image>
        {
            await CreateThumbnailImage(model.ThumbnailImage)
        };

        images.AddRange(await CreateImages(model.Images));

        newProduct.Images = images;
        await _productsRepository.AddAsync(newProduct);
        await _productsRepository.SaveChangesAsync();

        return newProduct.Id;
    }

    public async Task<ProductDetailsResponseDto> GetDetailsAsync(string productId)
    {
        var product = await _productsRepository
            .AllAsNoTracking()
            .Include(x => x.Creator)
            .Include(x => x.Images)
            .Include(x => x.Category)
            .Include(x => x.Creator.Image)
            .FirstOrDefaultAsync(x => x.Id == productId);

        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        var detailsModel = ProductDetailsResponseDto.FromProductDetailsResponseDto(product);

        return detailsModel;
    }

    public async Task EditProductAsync(EditProductRequestDto model)
    {
        var product = await _productsRepository
            .All()
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == model.ProductId);

        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        if (await IsProductMadeByUserAsync(model.ProductId, model.UserId) == false)
        {
            throw new InvalidOperationException("User cant edit a product not created by him.");
        }

        product.Title = model.Title;
        product.Description = model.Description;
        product.Price = model.Price;
        product.CategoryId = model.CategoryId;

        if (model.DiscountPrice > 0)
        {
            if (model.DiscountPrice >= product.Price)
            {
                throw new InvalidOperationException("Discount cannot be more than the actual price.");
            }

            product.IsOnDiscount = true;
            product.DiscountPrice = model.DiscountPrice;
        }
        else
        {
            product.IsOnDiscount = false;
            product.DiscountPrice = 0;
        }

        var images = new List<Image>();

        if (model.ThumbnailImage != null)
        {
            var imageToDelete = await _imagesService.GetProductThumbnail(product.Id);
            await _imagesService.DeleteImageAsync(imageToDelete.Id);
            var thumbnailImage = await CreateThumbnailImage(model.ThumbnailImage);
            images.Add(thumbnailImage);
        }

        if (model.Images != null && model.Images.Any())
        {
            var nonThumbnailImages = await _imagesService.GetProductNonThumbnailImages(product.Id);
            await DeleteImages(nonThumbnailImages);

            images.AddRange(await CreateImages(model.Images));
        }

        product.Images.AddRange(images);

        await _productsRepository.SaveChangesAsync();
    }
    public async Task<ProductEditDetailsResponseDto> GetEditDetailsAsync(GetProductEditDetailsRequestDto model)
    {
        var user = await _usersService.GetByIdAsync(model.UserId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var product = await _productsRepository
           .AllAsNoTracking()
           .FirstOrDefaultAsync(x => x.Id == model.ProductId);

        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        if (await IsProductMadeByUserAsync(model.ProductId, user.Id) == false)
        {
            throw new InvalidOperationException("User cannot delete product that is not made by him");
        }

        return ProductEditDetailsResponseDto.FromProdcut(product);
    }

    public async Task DeleteAsync(DeleteProductRequestDto model)
    {
        var user = await _usersService.GetByIdAsync(model.UserId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var product = await _productsRepository
            .All()
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == model.ProductId);

        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        if (await IsProductMadeByUserAsync(model.ProductId, user.Id) == false)
        {
            throw new InvalidOperationException("User cannot delete product that is not made by him");
        }

        _productsRepository.SoftDelete(product);

        await DeleteImages(product.Images);

        await _productsRepository.SaveChangesAsync();
    }

    public async Task<bool> IsProductMadeByUserAsync(string productId, string userId)
        => await _productsRepository.All()
        .FirstOrDefaultAsync(x => x.Id == productId && x.CreatorId == userId) == null ? false : true;

    public async Task<ProductListingResponseDto> GetUserOwnProductsAsync(UserOwnProductsRequestDto model)
    {
        var products = _productsRepository
            .AllAsNoTracking()
            .Include(x => x.Creator)
            .Include(x => x.Images)
            .Where(x => x.CreatorId == model.UserId)               
            .AsQueryable();


        products = FilterProducts(products, model.Search, model.MinPrice, model.MaxPrice, model.CategoryIds);
        products = OrderProductsBy(products, model.OrderBy);

        var totalItems = await products.CountAsync();

        var result = products.Skip((model.PageNumber - 1) * model.PageSize)
            .Take(model.PageSize).ToList();

        var productsResult = ProductListingResponseDto.FromProducts(result);
        productsResult.PageNumber = model.PageNumber;
        productsResult.TotalItems = totalItems;

        return productsResult;
    }

    public async Task PromoteAsync(PromoteProductRequestDto model)
    {
        var user = await _usersService.GetByIdAsync(model.UserId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var product = await _productsRepository
            .All()
            .FirstOrDefaultAsync(x => x.Id == model.ProductId);

        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        if (product.IsPromoted == true)
        {
            throw new InvalidOperationException("Product is already promoted.");
        }

        if (await IsProductMadeByUserAsync(model.ProductId, model.UserId) == false)
        {
            throw new InvalidOperationException("Only the owner can promote this product.");
        }

        var promotionPricing = await _promotionPricingService.GetByIdAsync(model.PromotionPricingId);

        if (promotionPricing == null)
        {
            throw new PromotionPricingNotFoundException();
        }

        product.IsPromoted = true;
        product.PromotedUntil = DateTime.UtcNow.AddDays(promotionPricing.DurationDays);

        await _productsRepository.SaveChangesAsync();
    }

    private async Task DeleteImages(IEnumerable<Image> images)
    {
        foreach (var image in images)
        {
            await _imagesService.DeleteImageAsync(image.Id);
        }
    }

    private async Task<List<Image>> CreateImages(List<IFormFile> images)
    {
        var imagesToReturn = new List<Image>();

        for (int i = 0; i < images.Count(); i++)
        {
            var currentNewImageId = await _imagesService.CreateImageAsync(images[i], SliderWidth, SliderHeight);
            var currentNewImage = await _imagesService.GetImageByIdAsync(currentNewImageId);

            imagesToReturn.Add(currentNewImage);
        }

        return imagesToReturn;
    }

    private async Task<Image> CreateThumbnailImage(IFormFile image)
    {
        var thumbnailImageId = await _imagesService.CreateImageAsync(image, ThumbnailWidth, ThumbnailHeight);
        var thumbnailImage = await _imagesService.GetImageByIdAsync(thumbnailImageId);

        thumbnailImage.IsThumbnail = true;

        return thumbnailImage;
    }

    private IQueryable<Product> FilterProducts(IQueryable<Product> products, string? search, decimal minPrice, decimal maxPrice, List<string> categoryIds)
    {
        if (search != null)
        {
            products = products.Where(x => x.Title.Contains(search));
        }

        if (minPrice > 0)
        {
            products = products.Where(x => x.Price >= minPrice);
        }

        if (maxPrice > 0 && maxPrice >= minPrice)
        {
            products = products.Where(x => x.Price <= maxPrice);
        }

        if (categoryIds.Any())
        {
            products = products.Where(x => categoryIds.Contains(x.CategoryId));
        }

        return products;
    }  

    public async Task<ProductListingResponseDto> SearchAsync(FilterPaginationDto model)
    {
        var products = _productsRepository
            .AllAsNoTracking()
            .Include(x => x.Creator)
            .Include(x => x.Images)
            .AsQueryable();


        products = FilterProducts(products, model.Search, model.MinPrice, model.MaxPrice, model.CategoryIds);
        products = OrderProductsBy(products, model.OrderBy);

        var totalItems = await products.CountAsync();

        var result = products.Skip((model.PageNumber - 1) * model.PageSize)
            .Take(model.PageSize).ToList();

        var productsResult = ProductListingResponseDto.FromProducts(result);
        productsResult.PageNumber = model.PageNumber;
        productsResult.TotalItems = totalItems;

        return productsResult;
    }

    public async Task<ProductListingResponseDto> GetUserProductsAsync(UserProductsRequestDto model)
    {
        var user = await _usersService.GetByIdAsync(model.UserId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var products = _productsRepository
            .AllAsNoTracking()
            .Include(x => x.Creator)
            .Include(x => x.Images)
            .Where(x => x.CreatorId == model.UserId)
            .AsQueryable();

        products = FilterProducts(products, model.Search, model.MinPrice, model.MaxPrice, model.CategoryIds);
        products = OrderProductsBy(products, model.OrderBy);

        var totalItems = await products.CountAsync();

        var result = products.Skip((model.PageNumber - 1) * model.PageSize)
            .Take(model.PageSize).ToList();

        var productsResult = ProductListingResponseDto.FromProducts(result);
        productsResult.PageNumber = model.PageNumber;
        productsResult.TotalItems = totalItems;

        return productsResult;
    }

    public Task<Product> GetByIdAsync(string id)
        => _productsRepository.All().FirstOrDefaultAsync(x => x.Id == id);

    private IQueryable<Product> OrderProductsBy(IQueryable<Product> products, string? orderBy)
    {
        switch (orderBy)
        {
            case "DateAsc":
                products = products
                    .OrderByDescending(x => x.IsPromoted)
                    .ThenBy(x => x.CreatedOn);
                break;
            case "PriceDesc":
                products = products
                    .OrderByDescending(x => x.IsPromoted)
                    .ThenByDescending(x => x.Price);
                break;
            case "PriceAsc":
                products = products
                    .OrderByDescending(x => x.IsPromoted)
                    .ThenBy(x => x.Price);
                break;
            default:
                products = products
                    .OrderByDescending(x => x.IsPromoted)
                    .ThenByDescending(x => x.CreatedOn);
                break;
        }

        return products;
    }
}



