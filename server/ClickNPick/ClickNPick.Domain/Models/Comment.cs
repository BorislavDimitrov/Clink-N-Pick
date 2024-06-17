using ClickNPick.Domain.Models.Common;

namespace ClickNPick.Domain.Models;

public class Comment : BaseModel<string>
{
    public Comment()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public string ProductId { get; set; }

    public virtual Product Product { get; set; }

    public string? ParentId { get; set; }

    public virtual Comment Parent { get; set; }

    public string Content { get; set; }

    public string CreatorId { get; set; }

    public virtual User Creator { get; set; }
}
