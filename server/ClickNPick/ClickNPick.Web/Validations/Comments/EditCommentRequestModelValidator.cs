using ClickNPick.Web.Models.Comments.Request;
using FluentValidation;

namespace ClickNPick.Web.Validations.Comments;

public class EditCommentRequestModelValidator : AbstractValidator<EditCommentRequestModel>
{
    public EditCommentRequestModelValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(2000).WithMessage("Name must be up to 2000 characters long.");

        RuleFor(x => x.CommentId)
            .NotEmpty().WithMessage("CommentId is required.");
    }
}
