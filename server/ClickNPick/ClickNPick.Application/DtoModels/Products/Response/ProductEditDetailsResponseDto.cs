using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Products.Response;

public class ProductEditDetailsResponseDto
{
    public string Title { get; set; }

    public decimal Price { get; set; }

    public decimal DiscountPrice { get; set; }

    public string CategoryId { get; set; }

    public string Description { get; set; }

    public static ProductEditDetailsResponseDto FromProdcut(Product product)
    {
        var dto = new ProductEditDetailsResponseDto();

        dto.Title = product.Title;
        dto.Price = product.Price;
        dto.CategoryId = product.CategoryId;
        dto.Description = product.Description;
        dto.DiscountPrice = product.DiscountPrice;

        return dto;
    }
}
