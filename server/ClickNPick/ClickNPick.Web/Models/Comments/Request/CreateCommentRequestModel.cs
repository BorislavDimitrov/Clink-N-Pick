using ClickNPick.Application.DtoModels.Comments.Request;

namespace ClickNPick.Web.Models.Comments.Request
{
    public class CreateCommentRequestModel
    {
        public string ProductId { get; set; }

        public string ParentId { get; set; }

        public string Content { get; set; }


        public CreateCommentRequestDto ToCreateCommentRequestDto()
        {
            return new CreateCommentRequestDto
            {
                ParentId = this.ParentId,
                Content = this.Content,
                ProductId = this.ProductId
            };
        }
    }
}
