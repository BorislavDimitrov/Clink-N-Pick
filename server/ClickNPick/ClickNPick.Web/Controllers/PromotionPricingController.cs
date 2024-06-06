using ClickNPick.Application.Services.PromotionPricings;
using ClickNPick.Web.Models.PromotionPrices.Response;
using Microsoft.AspNetCore.Mvc;

namespace ClickNPick.Web.Controllers;

public class PromotionPricingController : ApiController
{
    private readonly IPromotionPricingService _promotionPricingService;

    public PromotionPricingController(IPromotionPricingService promotionPricingService)
    {
        _promotionPricingService = promotionPricingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _promotionPricingService.GetAll();
        var response = PromotionListingResponseModel.FromPromotionListingResponseDto(result);

        return Ok(response);
    }
}
