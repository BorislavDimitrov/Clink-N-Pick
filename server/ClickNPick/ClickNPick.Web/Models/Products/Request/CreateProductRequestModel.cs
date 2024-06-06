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
        return new CreateProductRequestDto
        {
            Title = this.Title,
            Description = this.Description,
            Price = this.Price,
            CategoryId = this.CategoryId,
            ThumbnailImage = this.ThumbnailImage,
            Images = this.Images
        };
    }
}
