using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Products.Response;

public class ProductInListResponseDto
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string ImageUrl { get; set; }

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public bool IsOnDiscount { get; set; }

    public string CreatorName { get; set; }

    public bool IsPromoted { get; set; }

    public static ProductInListResponseDto FromProduct(Product product)
    {
        return new ProductInListResponseDto
        {
            Id = product.Id,
            Title = product.Title,
            ImageUrl = product.Images.Where(x => x.IsThumbnail && x.IsDeleted == false)?.FirstOrDefault()?.Url,
            Price = product.Price,
            DiscountPrice = product.DiscountPrice,
            IsOnDiscount = product.IsOnDiscount,
            CreatorName = product.Creator.UserName,
            IsPromoted = product.IsPromoted,
        };
    }
}
