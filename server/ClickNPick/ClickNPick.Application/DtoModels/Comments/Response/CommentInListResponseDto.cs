using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Comments.Response
{
    public class CommentInListResponseDto
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Content { get; set; }

        public string CreatorUsername { get; set; }

        public string CreatorImageUrl { get; set; }

        public string CreatorId { get; set; }

        public string CreatedOn { get; set; }

        public static CommentInListResponseDto FromComment(Comment comment)
        {
            return new CommentInListResponseDto
            {
                Id = comment.Id,
                ParentId = comment.ParentId,
                Content = comment.Content,
                CreatorUsername = comment.Creator.UserName,
                CreatorImageUrl = comment.Creator.Image.Url,
                CreatorId = comment.Creator.Id,
                CreatedOn = comment.CreatedOn.ToString(),
            };
        }
    }
}
