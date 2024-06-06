using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.PromotionPricings.Response;

public class PromotionListingResponseDto
{
    public List<PromotionInListResponseDto> Promotions { get; set; }

    public static PromotionListingResponseDto FromProducts(IEnumerable<PromotionPricing> promotions)
    {
        var promotionsDto = promotions.Select(x => PromotionInListResponseDto.FromPromotionPricing(x)).ToList();

        return new PromotionListingResponseDto { Promotions = promotionsDto };
    }
}
