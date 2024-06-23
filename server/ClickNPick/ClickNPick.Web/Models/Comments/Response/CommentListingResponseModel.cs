using ClickNPick.Application.DtoModels.Comments.Response;

namespace ClickNPick.Web.Models.Comments.Response;

public class CommentListingResponseModel
{
    public List<CommentInListResponseModel> Comments { get; set; }

    public static CommentListingResponseModel FromCommentListingResponseDto(CommentListingResponseDto dto)
    {
        var comments = dto.Comments.Select(x => CommentInListResponseModel.FromCommentInListResponseDto(x)).ToList();

        return new CommentListingResponseModel { Comments = comments };
    }
}
