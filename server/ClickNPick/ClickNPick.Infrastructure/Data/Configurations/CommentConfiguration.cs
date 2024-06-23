using ClickNPick.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClickNPick.Infrastructure.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> comment)
        {
            comment
                .HasOne(x => x.Product)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            comment
                .HasOne(c => c.Creator)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.CreatorId);

            comment
                .HasKey(x => x.Id);
        }
    }
}
