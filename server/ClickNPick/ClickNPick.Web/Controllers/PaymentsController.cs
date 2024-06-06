using ClickNPick.Application.Services.Payment;
using ClickNPick.Application.Services.PromotionPricings;
using ClickNPick.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ClickNPick.Web.Controllers;

public class PaymentsController : ApiController
{
    private readonly IPromotionPricingService _promotionPricingService;
    private readonly IPaymentService _stripeService;

    public PaymentsController(
        IPromotionPricingService promotionPricingService
        , IPaymentService stripeService)
    {
        _promotionPricingService = promotionPricingService;
        _stripeService = stripeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] string promotionId)
    {
        var promotion = await _promotionPricingService.GetByIdAsync(promotionId);
        var email = HttpContext.User.GetEmail();

        var clientSecret = await _stripeService.CreatePaymentIntent(promotion.Price, email);

        return Ok(new { clientSecret });
    }
}
