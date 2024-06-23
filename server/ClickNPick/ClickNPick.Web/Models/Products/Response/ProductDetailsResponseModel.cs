using ClickNPick.Application.DtoModels.Products.Response;

namespace ClickNPick.Web.Models.Products.Response;

public class ProductDetailsResponseModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsOnDiscount { get; set; }

    public decimal DiscountPrice { get; set; }

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
        var model = new ProductDetailsResponseModel();

        model.Title = dto.Title;
        model.Description = dto.Description;
        model.Price = dto.Price;
        model.IsOnDiscount = dto.IsOnDiscount;
        model.DiscountPrice = dto.DiscountPrice;
        model.CategoryId = dto.CategoryId;
        model.CategoryName = dto.CategoryName;
        model.CreatorId = dto.CreatorId;
        model.CreatorImageUrl = dto.CreatorImageUrl;
        model.CreatorUsername = dto.CreatorUsername;
        model.CreatorEmail = dto.CreatorEmail;
        model.CreatorPhoneNumber = dto.CreatorPhoneNumber;
        model.ImageUrls = dto.ImageUrls;

        return model;
    }
}
