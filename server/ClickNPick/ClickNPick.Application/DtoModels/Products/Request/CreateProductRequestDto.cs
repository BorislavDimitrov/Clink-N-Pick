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
        var product = new Product();

        product.Title = this.Title;
        product.Description = this.Description;
        product.Price = this.Price;
        product.CreatorId = this.CreatorId;
        product.CategoryId = this.CategoryId;
        
        return product;
    }
}
