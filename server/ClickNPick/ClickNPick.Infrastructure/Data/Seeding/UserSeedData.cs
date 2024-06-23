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

        modelBuilder.Entity<User>().HasData(
            new User { Id = UserAdminId, UserName = "Administrator", NormalizedUserName = "ADMINISTRATOR", PasswordHash = adminHashedPassword, Email = "admin@yopmail.com", NormalizedEmail = "ADMIN@YOPMAIL.COM", EmailConfirmed = true, ImageId = ImageSeedData.AdminImageId, }
        );
    }
}