using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities;

namespace Store.Data.EntityConfigurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("Products", "dbo");

        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(c => c.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETUTCDATE()");

        builder
            .Property(c => c.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);
    }
}