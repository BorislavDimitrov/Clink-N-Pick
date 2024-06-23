using ClickNPick.Web.Models.Categories.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Categories;

public class EditCategoryRequestModelValidator : AbstractValidator<EditCategoryRequestModel>
{
    public EditCategoryRequestModelValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Name is required.")
        .MaximumLength(20).WithMessage("Name must be up to 20 characters long.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is requried.");
    }
}
