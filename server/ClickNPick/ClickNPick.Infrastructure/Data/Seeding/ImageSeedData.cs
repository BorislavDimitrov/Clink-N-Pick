using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Infrastructure.Data.Seeding;

public class ImageSeedData
{
    public static readonly string AdminImageId = Guid.NewGuid().ToString();

    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>().HasData(
        new Image { Id = AdminImageId, Url = "https://res.cloudinary.com/dtaqyp4b6/image/upload/v1718898254/ApplicationImages/nudbusyqqrn7ciq5gaha.jpg", PublicId = "ge32_gre_4", UserId = UserSeedData.UserAdminId }
        );
    }
}
