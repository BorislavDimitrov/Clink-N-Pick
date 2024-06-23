using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickNPick.Infrastructure.Data.Configurations;

public class ShipmentRequestConfiguration : IEntityTypeConfiguration<ShipmentRequest>
{
    public void Configure(EntityTypeBuilder<ShipmentRequest> shipmentRequest)
    {
        shipmentRequest
            .HasOne(x => x.Product)
            .WithMany(x => x.ShipmentRequests)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        shipmentRequest
            .HasOne(s => s.Buyer)
            .WithMany(u => u.ShipmentsAsBuyer)
            .HasForeignKey(s => s.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        shipmentRequest
            .HasOne(s => s.Seller)
            .WithMany(u => u.ShipmentsAsSeller)
            .HasForeignKey(s => s.SellerId)
            .OnDelete(DeleteBehavior.Restrict);

        shipmentRequest
            .HasKey(x => x.Id);
    }
}
