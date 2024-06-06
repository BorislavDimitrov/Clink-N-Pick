using ClickNPick.Application.DtoModels.Products.Response;

namespace ClickNPick.Web.Models.Products.Response;

internal class ProductEditDetailsResponseModel
{
    public string Title { get; set; }

    public decimal Price { get; set; }

    public decimal DiscountPrice { get; set; }

    public string CategoryId { get; set; }

    public string Description { get; set; }

    public static ProductEditDetailsResponseModel FromProductEditDetailsResponseDto(ProductEditDetailsResponseDto dto)
    {
        return new ProductEditDetailsResponseModel
        {
            Title = dto.Title,
            Price = dto.Price,
            DiscountPrice = dto.DiscountPrice,
            CategoryId = dto.CategoryId,
            Description = dto.Description,
        };
    }
}
