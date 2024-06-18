using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Comments.Response
{
    public class CommentListingResponseDto
    {
        public List<CommentInListResponseDto> Comments { get; set; }

        public static CommentListingResponseDto FromComments(IEnumerable<Comment> comments)
        {
            var commentsDto = comments.Select(x => CommentInListResponseDto.FromComment(x)).ToList();

            return new CommentListingResponseDto { Comments = commentsDto };
        }
    }
}
