using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Comments.Request;

public class CreateCommentRequestDto
{
    public string ProductId { get; set; }

    public string ParentId { get; set; }

    public string Content { get; set; }

    public string CreatorId { get; set; }

    public Comment ToComment()
    {
        var comment = new Comment();

        comment.CreatorId = this.CreatorId;
        comment.Content = this.Content;
        comment.ParentId = this.ParentId;
        comment.ProductId = this.ProductId;

        return comment;
    }
}
