using ClickNPick.Application.DtoModels.Comments.Response;

namespace ClickNPick.Web.Models.Comments.Response;

public class CreateCommentResponseModel
{
    public string Id { get; set; }

    public string CreatorId { get; set; }

    public string Content { get; set; }

    public string ParentId { get; set; }

    public string CreatorUsername { get; set; }

    public string CreatorImageUrl { get; set; }

    public string CreatedOn { get; set; }

    public static CreateCommentResponseModel FromCreateCommentResponseDto(CreateCommentResponseDto dto)
    {
        var model = new CreateCommentResponseModel();

        model.Id = dto.Id;
        model.CreatorId = dto.CreatorId;
        model.Content = dto.Content;
        model.ParentId = dto.ParentId;
        model.CreatorUsername = dto.CreatorUsername;
        model.CreatorImageUrl = dto.CreatorImageUrl;
        model.CreatedOn = dto.CreatedOn;

        return model;
    }
}
