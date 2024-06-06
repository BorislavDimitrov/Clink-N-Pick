using ClickNPick.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Infrastructure.Data.Seeding;

public class UserSeedData
{
    public static readonly string UserAdminId = Guid.NewGuid().ToString();
    public static readonly string UserId = Guid.NewGuid().ToString();
    public static void Seed(ModelBuilder modelBuilder)
    {
        var hasher = new PasswordHasher<User>();
        var adminHashedPassword = hasher.HashPassword(null, "Mypassword1!");
        var userHashedPassword = hasher.HashPassword(null, "Mypassword1!");

        modelBuilder.Entity<User>().HasData(
            new User { Id = UserAdminId, UserName = "Adminovich", NormalizedUserName = "ADMINOVICH", PasswordHash = adminHashedPassword, Email = "admin@yopmail.com", NormalizedEmail = "ADMIN@YOPMAIL.COM", EmailConfirmed = true, ImageId = ImageSeedData.AdminImageId, },
            new User { Id = UserId, UserName = "Userovich", NormalizedUserName = "USEROVICH", PasswordHash = userHashedPassword, Email = "user@yopmail.com", NormalizedEmail = "USER@YOPMAIL.COM", EmailConfirmed = true, ImageId = ImageSeedData.UserImageId, }
        );
    }
}
