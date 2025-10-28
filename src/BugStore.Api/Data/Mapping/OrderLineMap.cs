using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Api.Data.Mapping;

public class OrderLineMap : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderLine> builder)
    {
        builder.ToTable("OrderLine");
        builder.HasKey(ol => ol.Id);

        builder.Property(ol => ol.Quantity)
            .IsRequired();

        builder.Property(ol => ol.Total)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(ol => ol.Product)
            .WithMany()
            .HasForeignKey(ol => ol.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Order>()
            .WithMany(o => o.Lines)
            .HasForeignKey(l => l.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
