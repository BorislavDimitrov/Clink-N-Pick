using ClickNPick.Application.DtoModels.Products.Response;

namespace ClickNPick.Web.Models.Products.Response;

public class ProductDetailsResponseModel
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

    public static ProductDetailsResponseModel FromProductDetailsResponseDto(ProductDetailsResponseDto dto)
    {
        return new ProductDetailsResponseModel
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            IsOnDiscount = dto.IsOnDiscount,
            DiscountPrice = dto.DiscountPrice,
            CategoryId = dto.CategoryId,
            CategoryName = dto.CategoryName,
            CreatorId = dto.CreatorId,
            CreatorImageUrl = dto.CreatorImageUrl,
            CreatorUsername = dto.CreatorUsername,
            CreatorEmail = dto.CreatorEmail,
            CreatorPhoneNumber = dto.CreatorPhoneNumber,
            ImageUrls = dto.ImageUrls,

        };
    }
}
