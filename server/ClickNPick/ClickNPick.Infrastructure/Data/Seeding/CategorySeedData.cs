using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Infrastructure.Data.Seeding;

public class CategorySeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
        new Category { Id = Guid.NewGuid().ToString(), Name = "Electronics"},
        new Category { Id = Guid.NewGuid().ToString(), Name = "Books"},
        new Category { Id = Guid.NewGuid().ToString(), Name = "Clothing" },
        new Category { Id = Guid.NewGuid().ToString(), Name = "Home Appliances" },
        new Category { Id = Guid.NewGuid().ToString(), Name = "Sports Equipment" },
        new Category { Id = Guid.NewGuid().ToString(), Name = "Furniture"},
        new Category { Id = Guid.NewGuid().ToString(), Name = "Toys" },
        new Category { Id = Guid.NewGuid().ToString(), Name = "Beauty Products" },
        new Category { Id = Guid.NewGuid().ToString(), Name = "Food & Beverages"},
        new Category { Id = Guid.NewGuid().ToString(), Name = "Office Supplies"},
        new Category { Id = Guid.NewGuid().ToString(), Name = "For The Car" }
        );
    }
}
