using AutoFixture;
using AutoFixture.AutoMoq;
using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.DtoModels.Products.Request;
using ClickNPick.Application.Exceptions.Identity;
using ClickNPick.Application.Services.Categories;
using ClickNPick.Application.Services.Images;
using ClickNPick.Application.Services.Products;
using ClickNPick.Application.Services.PromotionPricings;
using ClickNPick.Application.Services.Users;
using ClickNPick.Domain.Models;
using ClickNPick.Infrastructure.Data;
using ClickNPick.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ClickNPick.Tests.Services;

[TestFixture]
public class ProductServiceTests
{
    private IFixture _fixture;
    private ProductsService _productsService;
    private IRepository<Product> _productsRepository;
    private Mock<IUsersService> _mockUsersService;
    private Mock<IImagesService> _mockImagesService;
    private Mock<IPromotionPricingService> _mockPromotionPricingService;
    private Mock<ICategoriesService> _mockCategoriesService;
    private DbContextOptionsBuilder<ClickNPickDbContext> _options;
    private ClickNPickDbContext _context;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _mockUsersService = _fixture.Freeze<Mock<IUsersService>>();
        _mockImagesService = _fixture.Freeze<Mock<IImagesService>>();
        _mockPromotionPricingService = _fixture.Freeze<Mock<IPromotionPricingService>>();
        _mockCategoriesService = _fixture.Freeze<Mock<ICategoriesService>>();
        _options = new DbContextOptionsBuilder<ClickNPickDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new ClickNPickDbContext(_options.Options);
        _productsRepository = new Repository<Product>(_context);

        _productsService = _fixture.Create<ProductsService>();
    }

    [Test]
    public async Task CreateProductAsyncValidRequestCreatesProduct()
    {
        var model = new CreateProductRequestDto
        {
            CreatorId = "user1",
            CategoryId = "category1",
            ThumbnailImage = new FormFile(Stream.Null, 0, 0, "thumbnail", "thumbnail.jpg"),
            Images = new List<IFormFile> { new FormFile(Stream.Null, 0, 0, "thumbnail", "thumbnail.jpg"), new FormFile(Stream.Null, 0, 0, "thumbnail", "thumbnail.jpg") }
            
        };

        var user = new User { Id = "user1" };
        var category = new Category { Id = "category1" };
        var createdProduct = new Product { Id = "product1" };

        _mockUsersService.Setup(s => s.GetByIdAsync("user1")).ReturnsAsync(user);
        _mockCategoriesService.Setup(s => s.GetByIdAsync("category1")).ReturnsAsync(category);
        _mockImagesService.Setup(s => s.CreateImageAsync(It.IsAny<IFormFile>(), It.IsAny<int>(), It.IsAny<int>()))
                          .ReturnsAsync("thumbnailId");
        _mockImagesService.Setup(s => s.GetImageByIdAsync("thumbnailId")).ReturnsAsync(new Image { Id = "thumbnailId" });
 
        var result = await _productsService.CreateProductAsync(model);
        
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void CreateProductAsyncUserNotFoundThrowsUserNotFoundException()
    {
        var model = new CreateProductRequestDto { CreatorId = "nonExistingUser" };
        _mockUsersService.Setup(s => s.GetByIdAsync("nonExistingUser")).ReturnsAsync((User)null);

        Assert.ThrowsAsync<UserNotFoundException>(() => _productsService.CreateProductAsync(model));
    }

    [Test]
    public void EditProductAsync_UnauthorizedUser_ThrowsInvalidOperationException()
    {
        var model = new EditProductRequestDto { ProductId = "product1", UserId = "unauthorizedUser" };
        var product = new Product { Id = "product1", CreatorId = "user1" };

        Assert.ThrowsAsync<InvalidOperationException>(() => _productsService.EditProductAsync(model));
    }

    [Test]
    public void DeleteAsync_UnauthorizedUser_ThrowsInvalidOperationException()
    {
        var model = new DeleteProductRequestDto { ProductId = "product1", UserId = "unauthorizedUser" };
        var product = new Product { Id = "product1", CreatorId = "user1" };

        Assert.ThrowsAsync<InvalidOperationException>(() => _productsService.DeleteAsync(model));
    }

    [Test]
    public void GetEditDetailsAsyncNonExistingProductIdThrowsInvalidOperationException()
    {
        var model = new GetProductEditDetailsRequestDto { ProductId = "nonExistingProduct" };

        Assert.ThrowsAsync<InvalidOperationException>(() => _productsService.GetEditDetailsAsync(model));
    }

    [Test]
    public void GetEditDetailsAsyncUnauthorizedUserThrowsInvalidOperationException()
    {
        var model = new GetProductEditDetailsRequestDto { ProductId = "product1", UserId = "unauthorizedUser" };
        var product = new Product { Id = "product1", CreatorId = "user1" };

        Assert.ThrowsAsync<InvalidOperationException>(() => _productsService.GetEditDetailsAsync(model));
    }

    [Test]
    public void PromoteAsyncNonExistingProductIdThrowsInvalidOperationException()
    {
        var user = new User { Id = "user1" };
        _mockUsersService.Setup(s => s.GetByIdAsync("user1")).ReturnsAsync(user);

        var model = new PromoteProductRequestDto { ProductId = "nonExistingProduct" };

        Assert.ThrowsAsync<InvalidOperationException>(() => _productsService.PromoteAsync(model));
    }

    [Test]
    public void PromoteAsyncUnauthorizedUserThrowsInvalidOperationException()
    {
        var model = new PromoteProductRequestDto { ProductId = "product1", UserId = "unauthorizedUser" };
        var product = new Product { Id = "product1", CreatorId = "user1" };

        Assert.ThrowsAsync<InvalidOperationException>(() => _productsService.PromoteAsync(model));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    private IFormFile CreateFormFile(string path)
    {
        var fileInfo = new FileInfo(path);
        var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

        return new FormFile(fileStream, 0, fileStream.Length, fileInfo.Name, fileInfo.Name)
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };
    }
}
