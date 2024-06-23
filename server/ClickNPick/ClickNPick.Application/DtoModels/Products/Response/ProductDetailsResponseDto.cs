using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Products.Response;

public class ProductDetailsResponseDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsOnDiscount { get; set; }

    public decimal DiscountPrice { get; set; }

    public string CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string CreatorId { get; set; }

    public string CreatorImageUrl { get; set; }

    public string CreatorUsername { get; set; }

    public string CreatorPhoneNumber { get; set; }

    public string CreatorEmail { get; set; }

    public List<string> ImageUrls { get; set; }

    public static ProductDetailsResponseDto FromProduct(Product product)
    {
        var dto = new ProductDetailsResponseDto();

        dto.Title = product.Title;
        dto.Description = product.Description;
        dto.Price = product.Price;
        dto.IsOnDiscount = product.IsOnDiscount;
        dto.DiscountPrice = product.DiscountPrice;
        dto.CategoryId = product.CategoryId;
        dto.CategoryName = product.Category.Name;
        dto.CreatorId = product.CreatorId;
        dto.CreatorImageUrl = product.Creator.Image.Url;
        dto.CreatorUsername = product.Creator.UserName;
        dto.CreatorPhoneNumber = product.Creator.PhoneNumber;
        dto.CreatorEmail = product.Creator.Email;
        dto.ImageUrls = product.Images.Where(x => x.IsThumbnail == false).Select(x => x.Url).ToList();
        
        return dto;
    }
}
