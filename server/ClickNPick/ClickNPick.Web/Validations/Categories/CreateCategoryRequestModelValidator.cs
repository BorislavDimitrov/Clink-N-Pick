using ClickNPick.Web.Models.Categories.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Categories;

public class CreateCategoryRequestModelValidator : AbstractValidator<CreateCategoryRequestModel>
{
    public CreateCategoryRequestModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(20).WithMessage("Name must be up to 20 characters long.");
    }
}
