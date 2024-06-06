using ClickNPick.Web.Models.Users.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Users;

public class ChangePasswordRequestModelValidator : AbstractValidator<ChangePasswordRequestModel>
{
    public ChangePasswordRequestModelValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("OldPassword is required.")
            .MinimumLength(8).WithMessage("Old password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("Please enter old password with the following requirements: " +
            "Atleast 8 characters long, atleast one upper case letter, atleast one lower case letter, atleast one number and one special symbol.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("NewPassword is required.")
            .MinimumLength(8).WithMessage("New password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("Please enter new password with the following requirements: " +
            "Atleast 8 characters long, atleast one upper case letter, atleast one lower case letter, atleast one number and one special symbol.");
    }
}
