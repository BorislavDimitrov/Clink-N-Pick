using ClickNPick.Application.DtoModels.Comments.Request;

namespace ClickNPick.Web.Models.Comments.Request
{
    public class EditCommentRequestModel
    {
        public string CommentId { get; set; }

        public string Content { get; set; }

        public EditCommentRequestDto ToEditCommentRequestDto()
        {
            var dto = new EditCommentRequestDto();
            dto.CommentId = this.CommentId;
            dto.Content = this.Content;
            return dto;
        }
    }
}
