using ClickNPick.Web.Models.Users.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Users;

public class EmailConfirmationRequestModelValidator : AbstractValidator<EmailConfirmationRequestModel>
{
    public EmailConfirmationRequestModelValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id is reqired.");

        RuleFor(x => x.EmailConfirmationToken)
            .NotEmpty().WithMessage("Email confirmation token is required.");
    }
}
