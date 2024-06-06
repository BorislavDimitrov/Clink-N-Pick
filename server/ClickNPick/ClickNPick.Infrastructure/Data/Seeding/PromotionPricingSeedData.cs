using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Infrastructure.Data.Seeding;

public class PromotionPricingSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PromotionPricing>().HasData(
            new PromotionPricing { Id = Guid.NewGuid().ToString(), Price = 12, DurationDays = 7, Name = "Basic"},
            new PromotionPricing { Id = Guid.NewGuid().ToString(), Price = 20, DurationDays = 14 , Name = "Standart"},
            new PromotionPricing { Id = Guid.NewGuid().ToString(), Price = 30, DurationDays = 30, Name="Premium"}
        );
    }
}
