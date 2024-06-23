using Microsoft.AspNetCore.Http;

namespace ClickNPick.Application.DtoModels.Products.Request;

public class EditProductRequestDto
{
    public string ProductId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string CategoryId { get; set; }

    public decimal DiscountPrice { get; set; }

    public IFormFile ThumbnailImage { get; set; }

    public List<IFormFile> Images { get; set; }

    public string UserId { get; set; }
}
