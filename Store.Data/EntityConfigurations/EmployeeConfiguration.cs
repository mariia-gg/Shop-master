using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities;

namespace Store.Data.EntityConfigurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder
            .ToTable("Employees", "dbo");

        builder
            .HasKey(employee => employee.Id);

        builder
            .Property(employee => employee.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(employee => employee.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETUTCDATE()");

        builder
            .Property(employee => employee.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(employee => employee.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Ignore(employee => employee.FullName);

        builder
            .Property(employee => employee.Position)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder
            .Property(employee => employee.Salary)
            .IsRequired()
            .HasPrecision(9, 2);

        builder
            .Property(employee => employee.PhoneNumber)
            .HasMaxLength(20);

        builder
            .Property(employee => employee.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(employee => employee.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);
    }
}