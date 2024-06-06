using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickNPick.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> appUser)
    {
        appUser
            .HasMany(x => x.Products)
            .WithOne(x => x.Creator)
            .OnDelete(DeleteBehavior.Restrict);            

        appUser
            .HasOne(x => x.Image)
            .WithOne(x => x.User)
            .HasForeignKey<User>(x => x.ImageId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        appUser
           .HasMany(x => x.Claims)
           .WithOne()
           .HasForeignKey(x => x.UserId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

        appUser
            .HasMany(x => x.Logins)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        appUser
            .HasMany(x => x.Roles)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        appUser
             .HasKey(x => x.Id);
    }
}
