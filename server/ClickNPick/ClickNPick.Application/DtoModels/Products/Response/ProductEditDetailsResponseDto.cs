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
        return new ProductEditDetailsResponseDto
        {
            Title = product.Title,
            Price = product.Price,
            CategoryId = product.CategoryId,
            Description = product.Description,
            DiscountPrice = product.DiscountPrice
        };
    }
}
