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
        var dto = new PromotionInListResponseDto();

        dto.Price = promotion.Price;
        dto.DurationDays = promotion.DurationDays;
        dto.Name = promotion.Name;
        dto.Id = promotion.Id;
        dto.PricePerDay = Math.Round(promotion.PricePerDay, 2);
        
        return dto;
    }
}
