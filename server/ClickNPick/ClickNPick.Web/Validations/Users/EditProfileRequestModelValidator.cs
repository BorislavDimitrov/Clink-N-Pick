using ClickNPick.Web.Models.Users.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Users;

public class EditProfileRequestModelValidator : AbstractValidator<EditProfileRequestModel>
{      
    public EditProfileRequestModelValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(6).When(x => x.Username != null).WithMessage("Username must be atleast 6 characters long.");

        RuleFor(x => x.Bio)
            .MaximumLength(2000).When(x => x.Bio != null);

        RuleFor(x => x.Address)
            .MaximumLength(100).When(x => x.Address != null);

        RuleFor(x => x.Image)
            .Must(CommonValidations.BeAValidImage).When(x => x.Image != null).WithMessage("Only image files (jpg, jpeg, png) are allowed.")
            .Must(CommonValidations.BeWithinFileSizeLimit).When(x => x.Image != null).WithMessage($"Image size must be up to {CommonValidations.MaxFileSize / (1024 * 1024)} MB.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s.\0-9]*$").When(x => x.PhoneNumber != null).WithMessage("Enter a valid phone number.");
    }          
}
