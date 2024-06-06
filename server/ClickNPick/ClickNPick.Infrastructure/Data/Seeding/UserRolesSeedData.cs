using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Infrastructure.Data.Seeding;

public class UserRolesSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = RoleSeedData.AdminRoleId, UserId = UserSeedData.UserAdminId }
        );
    }
}
