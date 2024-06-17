using ClickNPick.Application.DtoModels.Comments.Response;

namespace ClickNPick.Web.Models.Comments.Response
{
    public class CreateCommentResponseModel
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string Content { get; set; }

        public string ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public static CreateCommentResponseModel FromCreateCommentResponseDto(CreateCommentResponseDto dto)
        {
            return new CreateCommentResponseModel
            {
                Id = dto.Id,
                CreatorId = dto.CreatorId,
                Content = dto.Content,
                ParentId = dto.ParentId,
                CreatedOn = dto.CreatedOn,
            };
        }
    }
}
