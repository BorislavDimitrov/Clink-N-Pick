using ClickNPick.Application.DtoModels.Products.Request;
using Microsoft.AspNetCore.Http;

namespace ClickNPick.Web.Models.Products.Request;

public class CreateProductRequestModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string CategoryId { get; set; }

    public IFormFile ThumbnailImage { get; set; }

    public List<IFormFile> Images { get; set; }

    public CreateProductRequestDto ToCreateProductRequestDto()
    {
        var dto = new CreateProductRequestDto();

        dto.Title = this.Title;
        dto.Description = this.Description;
        dto.Price = this.Price;
        dto.CategoryId = this.CategoryId;
        dto.ThumbnailImage = this.ThumbnailImage;
        dto.Images = this.Images;

        return dto;
    }
}
