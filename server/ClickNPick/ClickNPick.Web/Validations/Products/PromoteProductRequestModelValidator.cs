using ClickNPick.Web.Models.Products.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Products;

public class PromoteProductRequestModelValidator : AbstractValidator<PromoteProductRequestModel>
{
    public PromoteProductRequestModelValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product id is required.");

        RuleFor(x => x.PromotionPricingId)
            .NotEmpty().WithMessage("Promote pricing id is required.");
    }
}
