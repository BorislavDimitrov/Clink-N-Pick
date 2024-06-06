using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickNPick.Infrastructure.Data.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> image)
    {
        image
            .HasOne(x => x.User)
            .WithOne(x => x.Image)
            .HasForeignKey<Image>(x => x.UserId) 
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        image 
            .HasOne(x => x.Product)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.ProductId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        image
            .HasKey(x => x.Id);

        image
             .Property(x => x.Url)
             .IsRequired(true);
    }
}
