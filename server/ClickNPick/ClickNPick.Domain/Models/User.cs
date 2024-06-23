using ClickNPick.Domain.Models.Common;
using Microsoft.AspNetCore.Identity;

namespace ClickNPick.Domain.Models;

public class User : IdentityUser, IAuditInfo, ISoftDeletableModel
{
    public User()
    {
        Products = new List<Product>();
        ShipmentsAsSeller = new List<ShipmentRequest>();
        ShipmentsAsBuyer = new List<ShipmentRequest>();
        Comments = new List<Comment>();
        Roles = new HashSet<IdentityUserRole<string>>();
        Claims = new HashSet<IdentityUserClaim<string>>();
        Logins = new HashSet<IdentityUserLogin<string>>();
    }

    public string ImageId { get; set; }

    public virtual Image Image { get; set; }

    public string? Bio { get; set; }

    public string? Address { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<Product> Products { get; set; }

    public virtual ICollection<ShipmentRequest> ShipmentsAsBuyer { get; set; }

    public virtual ICollection<ShipmentRequest> ShipmentsAsSeller { get; set; }

    public virtual List<Comment> Comments { get; set; }

    public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } 

    public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
}
