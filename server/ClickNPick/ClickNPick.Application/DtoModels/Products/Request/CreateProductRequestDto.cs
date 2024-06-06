using ClickNPick.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace ClickNPick.Application.DtoModels.Products.Request;

public class CreateProductRequestDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string CreatorId { get; set; }

    public string CategoryId { get; set; }

    public IFormFile ThumbnailImage { get; set; }

    public List<IFormFile> Images { get; set; }

    public Product ToProduct()
    {
        return new Product
        {
            Title = this.Title,
            Description = this.Description,
            Price = this.Price,
            CreatorId = this.CreatorId,
            CategoryId = this.CategoryId,
        };
    }
}
