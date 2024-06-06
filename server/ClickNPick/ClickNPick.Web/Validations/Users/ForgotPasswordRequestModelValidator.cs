using ClickNPick.Web.Models.Users.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Users;

public class ForgotPasswordRequestModelValidator : AbstractValidator<ForgotPasswordRequestModel>
{
    public ForgotPasswordRequestModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is reqired.")
            .EmailAddress().WithMessage("Please enter a valid email address.");
    }
}
