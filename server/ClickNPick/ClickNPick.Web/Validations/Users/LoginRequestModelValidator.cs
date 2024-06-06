using ClickNPick.Web.Models.Users.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Users;

public class LoginRequestModelValidator : AbstractValidator<LoginRequestModel>
{
    public LoginRequestModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is reqired.")
            .EmailAddress().WithMessage("Please enter a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("Please enter password with the following requirements: " +
            "Atleast 8 characters long, atleast one upper case letter, atleast one lower case letter, atleast one number and one special symbol.");
    }
}
