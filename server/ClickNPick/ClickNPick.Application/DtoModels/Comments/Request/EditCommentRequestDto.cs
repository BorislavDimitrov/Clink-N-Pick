namespace ClickNPick.Application.DtoModels.Comments.Request
{
    public class EditCommentRequestDto
    {
        public string CommentId { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
