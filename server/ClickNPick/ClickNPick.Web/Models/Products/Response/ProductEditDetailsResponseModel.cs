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
        var model = new ProductEditDetailsResponseModel();

        model.Title = dto.Title;
        model.Price = dto.Price;
        model.DiscountPrice = dto.DiscountPrice;
        model.CategoryId = dto.CategoryId;
        model.Description = dto.Description;
        
        return model;
    }
}
