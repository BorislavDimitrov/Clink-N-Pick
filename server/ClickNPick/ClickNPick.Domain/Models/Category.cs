using ClickNPick.Domain.Models.Common;

namespace ClickNPick.Domain.Models;

public class Category : BaseModel<string>
{
    public Category()
    {
        Id = Guid.NewGuid().ToString();
        Products = new List<Product>();
    }

    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
}
