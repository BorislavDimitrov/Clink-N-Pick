using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickNPick.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> product)
    {
        product
            .HasOne(x => x.Creator)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CreatorId) 
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        product
           .HasOne(x => x.Category)
           .WithMany(x => x.Products)
           .HasForeignKey(x => x.CategoryId)
           .IsRequired(true)
           .OnDelete(DeleteBehavior.Restrict);

        product
            .HasMany(x => x.ShipmentRequests)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
            
       product
            .HasKey(x => x.Id);

        product
             .Property(x => x.Title)
             .IsRequired(true)
             .HasMaxLength(30);

        product
            .Property(x => x.Description)
            .IsRequired(true)
            .HasMaxLength(2000);

        product
            .Property(x => x.Price)
            .IsRequired(true);

        product
            .HasIndex(x => x.Price);

        product
            .HasIndex(x => x.CategoryId);

        product
            .HasIndex(x => x.Title);

        product
            .HasIndex(x => x.CreatedOn);
    }
}
