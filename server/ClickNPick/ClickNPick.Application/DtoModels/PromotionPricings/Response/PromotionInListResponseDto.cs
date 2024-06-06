using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.PromotionPricings.Response;

public class PromotionInListResponseDto
{
    public decimal Price { get; set; }

    public int DurationDays { get; set; }

    public string Name { get; set; }

    public decimal PricePerDay { get; set; }

    public string Id { get; set; }

    public static PromotionInListResponseDto FromPromotionPricing(PromotionPricing promotion)
    {
        return new PromotionInListResponseDto
        {
            Price = promotion.Price, 
            DurationDays = promotion.DurationDays,
            Name = promotion.Name,
            Id = promotion.Id,
            PricePerDay = Math.Round(promotion.PricePerDay, 2)
        };
    }
}
