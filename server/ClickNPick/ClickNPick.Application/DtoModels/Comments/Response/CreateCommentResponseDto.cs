using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Comments.Response
{
    public class CreateCommentResponseDto
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string Content { get; set; }

        public string ParentId { get; set; }

        public string CreatorUsername { get; set; }

        public string CreatorImageUrl { get; set; }

        public string CreatedOn { get; set; }

        public static CreateCommentResponseDto FromComment(Comment comment)
        {
            return new CreateCommentResponseDto
            {
                Id = comment.Id,
                CreatorId = comment.CreatorId,
                ParentId = comment.ParentId,
                Content = comment.Content,
                CreatorUsername = comment.Creator.UserName,
                CreatorImageUrl = comment.Creator.Image.Url,
                CreatedOn = comment.CreatedOn.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            };
        }
    }
}
