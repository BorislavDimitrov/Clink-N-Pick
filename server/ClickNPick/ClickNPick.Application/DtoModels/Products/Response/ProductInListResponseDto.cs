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

    public string CategoryName { get; set; }

    public bool IsPromoted { get; set; }

    public static ProductInListResponseDto FromProduct(Product product)
    {
        var dto = new ProductInListResponseDto();

        dto.Id = product.Id;
        dto.Title = product.Title;
        dto.ImageUrl = product.Images.Where(x => x.IsThumbnail && x.IsDeleted == false)?.FirstOrDefault()?.Url;
        dto.Price = product.Price;
        dto.DiscountPrice = product.DiscountPrice;
        dto.IsOnDiscount = product.IsOnDiscount;
        dto.CreatorName = product.Creator.UserName;
        dto.CategoryName = product.Category.Name;
        dto.IsPromoted = product.IsPromoted;

        return dto;
    }
}
