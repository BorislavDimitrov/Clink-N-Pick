using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Comments.Response
{
    public class CreateCommentResponseDto
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string Content { get; set; }

        public string ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public static CreateCommentResponseDto FromComment(Comment comment)
        {
            return new CreateCommentResponseDto
            {
                Id = comment.Id,
                CreatorId = comment.CreatorId,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
            };
        }
    }
}
