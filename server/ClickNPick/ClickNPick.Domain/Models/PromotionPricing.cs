using ClickNPick.Domain.Models.Common;

namespace ClickNPick.Domain.Models;

public class PromotionPricing : BaseModel<string>
{
    public PromotionPricing()
    {
        Id = Guid.NewGuid().ToString();
    }
    public decimal Price { get; set; }

    public int DurationDays { get; set; }

    public string Name { get; set; }

    public decimal PricePerDay { get; set; }

}
