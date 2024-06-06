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
        return new EditProductRequestDto
        {
            Title = this.Title,
            Description = this.Description,
            Price = this.Price,
            CategoryId = this.CategoryId,
            DiscountPrice = this.DiscountPrice,
            ThumbnailImage = this.ThumbnailImage,
            Images = this.Images,
            ProductId = this.ProductId,
        };          
    }
}
