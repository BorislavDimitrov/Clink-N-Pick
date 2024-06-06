using ClickNPick.Web.Models.Products.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Products;

public class CreateProductRequestModelValidator : AbstractValidator<CreateProductRequestModel>
{
    public CreateProductRequestModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(5).WithMessage("Title must be atleast 5 characters long")
            .MaximumLength(30).WithMessage("Title must be up to 30 characters long.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description must be up to 2000 characters long.");

        RuleFor(x => x.Price)
            .NotNull().WithMessage("Price is required.")
            .InclusiveBetween(1, 50_000).WithMessage("Price must be between 1 and 50,000.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category id is required.");

        RuleFor(x => x.ThumbnailImage)
            .Must(CommonValidations.BeAValidImage).WithMessage("Only image files (jpg, jpeg, png) are allowed.")
            .Must(CommonValidations.BeWithinFileSizeLimit).WithMessage($"Image size must be up to {CommonValidations.MaxFileSize / (1024 * 1024)} MB.");

        RuleFor(x => x.Images)
            .NotNull().WithMessage("Atleast one image is required.")
            .Must(images => images.Count <= CommonValidations.MaxImagesCount)
            .WithMessage("You can upload a maximum of 10 images.");

        RuleForEach(x => x.Images)
            .Must(CommonValidations.BeAValidImage).WithMessage("Only image files (jpg, jpeg, png) are allowed.")
            .Must(CommonValidations.BeWithinFileSizeLimit).WithMessage($"Each image size must be up to {CommonValidations.MaxFileSize / (1024 * 1024)} MB.");
    }
}
