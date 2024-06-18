using ClickNPick.Application.DtoModels.Comments.Response;

namespace ClickNPick.Web.Models.Comments.Response
{
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
            return new CreateCommentResponseModel
            {
                Id = dto.Id,
                CreatorId = dto.CreatorId,
                Content = dto.Content,
                ParentId = dto.ParentId,
                CreatorUsername = dto.CreatorUsername,
                CreatorImageUrl = dto.CreatorImageUrl,
                CreatedOn = dto.CreatedOn,
            };
        }
    }
}
