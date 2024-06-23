using ClickNPick.Application.DtoModels.Products.Response;

namespace ClickNPick.Web.Models.Products.Response;

public class ProductsInListResponseModel
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string ImageUrl { get; set; }

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public bool IsOnDiscount { get; set; }

    public string CreatorName { get; set; }

    public string CategoryName { get; set; }

    public bool IsPromoted { get; set; }

    public static ProductsInListResponseModel FromProductInListResponseDto(ProductInListResponseDto dto)
    {
        var model = new ProductsInListResponseModel();

        model.Id = dto.Id;
        model.Title = dto.Title;
        model.ImageUrl = dto.ImageUrl;
        model.Price = dto.Price;
        model.DiscountPrice = dto.DiscountPrice;
        model.IsOnDiscount = dto.IsOnDiscount;
        model.CreatorName = dto.CreatorName;
        model.IsPromoted = dto.IsPromoted;
        model.CategoryName = dto.CategoryName;

        return model;
    }
}
