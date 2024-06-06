using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickNPick.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> category)
    {
        category
            .HasMany(x => x.Products)
            .WithOne(x => x.Category)
            .OnDelete(DeleteBehavior.Restrict);

        category
            .HasKey(x => x.Id);

        category
            .HasIndex(x => x.Id)
            .IsUnique();

        category
            .Property(x => x.Name)
            .IsRequired(true)
            .HasMaxLength(35);                        
    }
}
