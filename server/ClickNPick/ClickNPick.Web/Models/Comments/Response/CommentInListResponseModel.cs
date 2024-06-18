using ClickNPick.Application.DtoModels.Comments.Response;

namespace ClickNPick.Web.Models.Comments.Response
{
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
            return new CommentInListResponseModel
            {
                Id = dto.Id,
                ParentId = dto.ParentId,
                Content = dto.Content,
                CreatorUsername = dto.CreatorUsername,
                CreatorImageUrl = dto.CreatorImageUrl,
                CreatorId = dto.CreatorId,
                CreatedOn = dto.CreatedOn,
            };
        }
    }
}
