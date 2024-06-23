using ClickNPick.Application.DtoModels.Comments.Response;

namespace ClickNPick.Web.Models.Comments.Response;

public class CommentInListResponseModel
{
    public string Id { get; set; }

    public string ParentId { get; set; }

    public string Content { get; set; }

    public string CreatorUsername { get; set; }

    public string CreatorImageUrl { get; set; }

    public string CreatorId { get; set; }

    public string CreatedOn { get; set; }

    public static CommentInListResponseModel FromCommentInListResponseDto(CommentInListResponseDto dto)
    {
        var model = new CommentInListResponseModel();

        model.Id = dto.Id;
        model.ParentId = dto.ParentId;
        model.Content = dto.Content;
        model.CreatorUsername = dto.CreatorUsername;
        model.CreatorImageUrl = dto.CreatorImageUrl;
        model.CreatorId = dto.CreatorId;
        model.CreatedOn = dto.CreatedOn;

        return model;
    }
}
