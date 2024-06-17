using ClickNPick.Domain.Models;

namespace ClickNPick.Application.DtoModels.Comments.Request
{
    public class CreateCommentRequestDto
    {

        public string ProductId { get; set; }

        public string ParentId { get; set; }

        public string Content { get; set; }

        public string CreatorId { get; set; }

        public Comment ToComment()
        {
            return new Comment
            {
                CreatorId = this.CreatorId,
                Content = this.Content,
                ParentId = this.ParentId,
                ProductId = this.ProductId
            };
        }
    }
}
