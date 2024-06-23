using ClickNPick.Application.Exceptions.General;

namespace ClickNPick.Application.Exceptions.Comments;

public class CommentNotFoundException : NotFoundException
{
    private const string DefaultMessage = "Comment not found.";

    public CommentNotFoundException() : base(DefaultMessage) { }

    public CommentNotFoundException(string message) : base(message) { }
}
