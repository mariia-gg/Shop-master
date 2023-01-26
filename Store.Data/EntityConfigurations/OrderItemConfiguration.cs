using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities;

namespace Store.Data.EntityConfigurations;

internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder
            .ToTable("OrderItems", "dbo");

        builder
            .HasKey(orderItem => orderItem.Id);

        builder
            .Property(orderItem => orderItem.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(orderItem => orderItem.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETUTCDATE()");

        builder
            .Ignore(orderItem => orderItem.TotalPrice);

        builder
            .HasOne(orderItem => orderItem.Product)
            .WithMany()
            .HasForeignKey(orderItem => orderItem.ProductId)
            .HasPrincipalKey(product => product.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}