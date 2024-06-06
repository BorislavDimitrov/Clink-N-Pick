using ClickNPick.Web.Models.Users.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Users;

public class ResetPasswordRequestModelValidator : AbstractValidator<ResetPasswordRequestModel>
{
    public ResetPasswordRequestModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is reqired.")
            .EmailAddress().WithMessage("Please enter a valid email address.");

        RuleFor(x => x.ResetPasswordToken)
            .NotEmpty().WithMessage("Reset password token is required.");

        RuleFor(x => x.Password)
        .NotEmpty().WithMessage("Password is required")
        .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
        .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("Please enter password with the following requirements: " +
            "Atleast 8 characters long, atleast one upper case letter, atleast one lower case letter, atleast one number and one special symbol.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.Password).WithMessage("Confirm password must match the new password");
    }
}
