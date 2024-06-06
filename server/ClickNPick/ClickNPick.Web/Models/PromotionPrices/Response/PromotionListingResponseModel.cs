using ClickNPick.Application.DtoModels.PromotionPricings.Response;

namespace ClickNPick.Web.Models.PromotionPrices.Response;

public class PromotionListingResponseModel
{
    public List<PromotionInListResponseModel> Promotions { get; set; }

    public static PromotionListingResponseModel FromPromotionListingResponseDto(PromotionListingResponseDto dto)
    {
        var promotions = dto.Promotions.Select(x => PromotionInListResponseModel.FromPromotionInListResponseDto(x)).ToList();

        return new PromotionListingResponseModel {  Promotions = promotions };
    }
}
