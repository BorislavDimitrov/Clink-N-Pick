using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickNPick.Infrastructure.Data.Configurations;

public class PromotionPricingConfiguration : IEntityTypeConfiguration<PromotionPricing>
{
    public void Configure(EntityTypeBuilder<PromotionPricing> promotionPricing)
    {
        promotionPricing
            .HasKey(x => x.Id);

        promotionPricing
            .Property(x => x.Name)
            .IsRequired(true)
            .HasMaxLength(15);

        promotionPricing
            .Property(x => x.Price)
            .IsRequired(true);
            
        promotionPricing
            .Property(x => x.DurationDays)
            .IsRequired(true)
            .HasMaxLength(30);

        promotionPricing
            .Property(x => x.PricePerDay)
            .IsRequired(true)
            .HasComputedColumnSql("[Price] / [DurationDays]", stored: true);
    }
}
