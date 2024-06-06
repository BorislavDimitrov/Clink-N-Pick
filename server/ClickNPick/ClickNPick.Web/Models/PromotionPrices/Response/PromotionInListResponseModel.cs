using ClickNPick.Application.DtoModels.PromotionPricings.Response;

namespace ClickNPick.Web.Models.PromotionPrices.Response;

public class PromotionInListResponseModel
{
    public decimal Price { get; set; }

    public int DurationDays { get; set; }

    public string Name { get; set; }

    public decimal PricePerDay { get; set; }

    public string Id { get; set; }

    public static PromotionInListResponseModel FromPromotionInListResponseDto(PromotionInListResponseDto dto)
    {
        return new PromotionInListResponseModel
        {
            Id = dto.Id,
            Price = dto.Price,
            DurationDays = dto.DurationDays,
            Name = dto.Name,
            PricePerDay = dto.PricePerDay,
        };
    }
}
