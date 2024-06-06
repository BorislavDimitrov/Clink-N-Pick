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

    public bool IsPromoted { get; set; }

    public static ProductsInListResponseModel FromProductInListResponseDto(ProductInListResponseDto dto)
    {
        return new ProductsInListResponseModel
        {
            Id = dto.Id,
            Title = dto.Title,
            ImageUrl = dto.ImageUrl,
            Price = dto.Price,
            DiscountPrice = dto.DiscountPrice,
            IsOnDiscount = dto.IsOnDiscount,
            CreatorName = dto.CreatorName,
            IsPromoted = dto.IsPromoted
        };
    }
}
