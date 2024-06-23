using ClickNPick.Application.DtoModels.PromotionPricings.Response;
using ClickNPick.Domain.Models;

namespace ClickNPick.Application.Services.PromotionPricings;

public interface IPromotionPricingService
{
    Task<PromotionPricing> GetByIdAsync(string promotionPricingId);

    Task<PromotionListingResponseDto> GetAllAsync();
}
