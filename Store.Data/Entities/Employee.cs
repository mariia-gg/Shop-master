using Store.Data.Enums;

namespace Store.Data.Entities;

public class Employee : BaseEntity, IEntity
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";

    public EmployeePosition Position { get; set; }

    public decimal Salary { get; set; }

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}