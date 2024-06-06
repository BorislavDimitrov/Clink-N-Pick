using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Products.Response;

public class ProductDetailsResponseDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsOnDiscount { get; set; }

    public decimal? DiscountPrice { get; set; }

    public string CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string CreatorId { get; set; }

    public string CreatorImageUrl { get; set; }

    public string CreatorUsername { get; set; }

    public string CreatorPhoneNumber { get; set; }

    public string CreatorEmail { get; set; }

    public List<string> ImageUrls { get; set; }

    public static ProductDetailsResponseDto FromProductDetailsResponseDto(Product product)
    {
        return new ProductDetailsResponseDto
        {
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            IsOnDiscount = product.IsOnDiscount,
            DiscountPrice = product.DiscountPrice,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.Name,
            CreatorId = product.CreatorId,
            CreatorImageUrl = product.Creator.Image.Url,
            CreatorUsername = product.Creator.UserName,
            CreatorPhoneNumber = product.Creator.PhoneNumber,
            CreatorEmail = product.Creator.Email,
            ImageUrls = product.Images.Where(x => x.IsThumbnail == false).Select(x => x.Url).ToList()
        };
    }
}
