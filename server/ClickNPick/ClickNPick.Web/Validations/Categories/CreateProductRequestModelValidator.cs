using ClickNPick.Web.Models.Categories.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Categories;

public class CreateProductRequestModelValidator : AbstractValidator<CreateCategoryRequestModel>
{
    public CreateProductRequestModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Title is required.");
    }
}
