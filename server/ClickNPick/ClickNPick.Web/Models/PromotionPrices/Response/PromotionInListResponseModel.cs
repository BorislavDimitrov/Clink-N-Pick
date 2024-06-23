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
        var model = new PromotionInListResponseModel();

        model.Id = dto.Id;
        model.Price = dto.Price;
        model.DurationDays = dto.DurationDays;
        model.Name = dto.Name;
        model.PricePerDay = dto.PricePerDay;

        return model;
    }
}
