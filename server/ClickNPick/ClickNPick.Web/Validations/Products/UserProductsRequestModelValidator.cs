using ClickNPick.Web.Models.Products.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Products;

public class UserProductsRequestModelValidator : AbstractValidator<UserProductsRequestModel>
{
    public UserProductsRequestModelValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id is required.");
    }
}
