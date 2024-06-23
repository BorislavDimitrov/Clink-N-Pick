using ClickNPick.Application.DtoModels.Comments.Request;

namespace ClickNPick.Web.Models.Comments.Request;

public class CreateCommentRequestModel
{
    public string ProductId { get; set; }

    public string ParentId { get; set; }

    public string Content { get; set; }

    public CreateCommentRequestDto ToCreateCommentRequestDto()
    {
        var dto = new CreateCommentRequestDto();

        dto.ParentId = this.ParentId;
        dto.Content = this.Content;
        dto.ProductId = this.ProductId;

        return dto;
    }
}
