using ClickNPick.Application.DtoModels.Products.Request;
using Microsoft.AspNetCore.Http;

namespace ClickNPick.Web.Models.Products.Request;

public class EditProductRequestModel
{
    public string ProductId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string CategoryId { get; set; }

    public decimal DiscountPrice { get; set; }

    public IFormFile ThumbnailImage { get; set; }

    public List<IFormFile> Images { get; set; }

    public EditProductRequestDto ToEditProductRequestDto()
    {
        var dto = new EditProductRequestDto();

        dto.Title = this.Title;
        dto.Description = this.Description;
        dto.Price = this.Price;
        dto.CategoryId = this.CategoryId;
        dto.DiscountPrice = this.DiscountPrice;
        dto.ThumbnailImage = this.ThumbnailImage;
        dto.Images = this.Images;
        dto.ProductId = this.ProductId;

        return dto;
    }
}
