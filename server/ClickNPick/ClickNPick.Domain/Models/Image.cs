using ClickNPick.Domain.Models.Common;

namespace ClickNPick.Domain.Models;

public class Image : BaseModel<string>
{
    public Image()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Url { get; set; }

    public string PublicId { get; set; }

    public string UserId { get; set; }

    public virtual User User { get; set; }      

    public string ProductId { get; set; }

    public virtual Product Product { get; set; }

    public bool IsThumbnail { get; set; } 
}
