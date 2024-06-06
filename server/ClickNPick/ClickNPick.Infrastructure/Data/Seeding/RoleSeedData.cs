using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Infrastructure.Data.Seeding;

public class RoleSeedData
{
    public static readonly string AdminRoleId = Guid.NewGuid().ToString();
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = AdminRoleId, Name = "Administrator", NormalizedName = "ADMINISTRATOR" }
        );
    }
}
