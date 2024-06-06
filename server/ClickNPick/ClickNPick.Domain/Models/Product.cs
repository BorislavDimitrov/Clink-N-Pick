using ClickNPick.Domain.Models.Common;

namespace ClickNPick.Domain.Models;

public class Product : BaseModel<string>
{
    public Product()
    {
        Id = Guid.NewGuid().ToString();
        Images = new List<Image>();
    }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsOnDiscount { get; set; }

    public decimal DiscountPrice { get; set; }

    public string CreatorId { get; set; }

    public bool IsPromoted { get; set; }

    public DateTime? PromotedUntil { get; set; }

    public User Creator { get; set; }

    public string CategoryId { get; set; }

    public Category Category { get; set; }

    public List<Image> Images { get; set; }

    public List<ShipmentRequest> ShipmentRequests { get; set; }
}
