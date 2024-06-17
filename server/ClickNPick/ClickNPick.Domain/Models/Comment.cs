namespace ClickNPick.Domain.Models;

public class Comment
{
    public int ProductId { get; set; }

    public virtual Product Product { get; set; }

    public int? ParentId { get; set; }

    public virtual Comment Parent { get; set; }

    public string Content { get; set; }

    public string CreatorId { get; set; }

    public virtual User Creator { get; set; }
}
