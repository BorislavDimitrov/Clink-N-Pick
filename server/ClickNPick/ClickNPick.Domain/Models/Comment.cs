namespace ClickNPick.Domain.Models;

public class Comment
{
    public string ProductId { get; set; }

    public virtual Product Product { get; set; }

    public string? ParentId { get; set; }

    public virtual Comment Parent { get; set; }

    public string Content { get; set; }

    public string CreatorId { get; set; }

    public virtual User Creator { get; set; }
}
