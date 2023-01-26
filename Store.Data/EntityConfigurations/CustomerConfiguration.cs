using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities;

namespace Store.Data.EntityConfigurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .ToTable("Customers", "dbo");

        builder
            .HasKey(customer => customer.Id);

        builder
            .Property(customer => customer.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(customer => customer.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETUTCDATE()");

        builder
            .Property(customer => customer.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(customer => customer.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(customer => customer.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(customer => customer.PasswordHash)
            .IsRequired()
            .HasPrecision(500);

        builder
            .Ignore(customer => customer.FullName);

        builder
            .Property(customer => customer.PhoneNumber)
            .HasMaxLength(20);

        builder
            .Property(customer => customer.Address)
            .HasMaxLength(100);

        builder
            .Property(customer => customer.City)
            .HasMaxLength(30);

        builder
            .Property(customer => customer.State)
            .HasMaxLength(20);

        builder
            .Property(customer => customer.ZipCode)
            .HasMaxLength(30);

        builder
            .Property(customer => customer.Country)
            .HasMaxLength(30);

        builder
            .HasMany(customer => customer.Orders)
            .WithOne(order => order.Customer)
            .HasForeignKey(order => order.CustomerId)
            .HasPrincipalKey(customer => customer.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}