using ClickNPick.Domain.Models.Common;

namespace ClickNPick.Domain.Models;

public class Product : BaseModel<string>
{
    public Product()
    {
        Id = Guid.NewGuid().ToString();
        Images = new List<Image>();
        ShipmentRequests = new List<ShipmentRequest>();
        Comments = new List<Comment>();
    }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public bool IsOnDiscount { get; set; }

    public decimal DiscountPrice { get; set; }

    public string CreatorId { get; set; }

    public bool IsPromoted { get; set; }

    public DateTime? PromotedUntil { get; set; }

    public virtual User Creator { get; set; }

    public string CategoryId { get; set; }

    public virtual Category Category { get; set; }

    public virtual List<Image> Images { get; set; }

    public virtual List<ShipmentRequest> ShipmentRequests { get; set; }

    public virtual List<Comment> Comments { get; set; }
}
