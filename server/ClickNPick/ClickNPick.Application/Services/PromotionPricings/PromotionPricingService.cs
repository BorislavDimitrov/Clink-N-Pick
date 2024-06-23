using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Application.DtoModels.PromotionPricings.Response;
using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Application.Services.PromotionPricings;

public class PromotionPricingService : IPromotionPricingService
{
    private readonly IRepository<PromotionPricing> _promotionPricingRepository;

    public PromotionPricingService(IRepository<PromotionPricing> promotionPricingRepository)
    {
        _promotionPricingRepository = promotionPricingRepository;
    }

    public async Task<PromotionListingResponseDto> GetAllAsync()
    {
        var promotions = await _promotionPricingRepository
            .AllAsNoTracking()
            .OrderBy(x => x.Price)
            .ToListAsync();

        return PromotionListingResponseDto.FromProducts(promotions);
    }

    public async Task<PromotionPricing> GetByIdAsync(string promotionPricingId)
        => await _promotionPricingRepository
        .All()
        .FirstOrDefaultAsync(x => x.Id == promotionPricingId);
}
