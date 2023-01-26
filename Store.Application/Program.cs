using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Data.Infrastructure;

await using var context = new StoreContext();

var customerRepository = new Repository<Customer>(context);

//var addedCustomer = await customerRepository.AddAsync(new Customer
//{
//    FirstName = "John",
//    LastName = "Doe",
//    Email = "john.doe@gmail.com",
//    PasswordHash = PasswordHasher.HashPassword("Pa$$word123"),
//    Orders = new List<Order>
//    {
//        new()
//        {
//            OrderItems = new List<OrderItem>
//            {
//                new()
//                {
//                    Product = new Product
//                    {
//                        Name = "Milk",
//                        Description = "Under cow",
//                        Price = 1.99m
//                    },
//                    Quantity = 10
//                }
//            }
//        }
//    }
//});

//await context.SaveChangesAsync();

var employeeRepository = new Repository<Employee>(context);

//var addedEmployee = await employeeRepository.AddAsync(new Employee
//{
//    FirstName = "Jane",
//    LastName = "Doe",
//    Position = EmployeePosition.Manager,
//    Salary = 100000,
//    Email = "jane.doe@gmail.com",
//    PasswordHash = PasswordHasher.HashPassword("Pa$$word123"),
//    PhoneNumber = "+380987654321"
//});

//await employeeRepository.SaveChangesAsync();

var customers = await customerRepository
    .Query()
    .Include(customer => customer.Orders)!
    .ThenInclude(order => order.OrderItems)!
    .ThenInclude(orderItem => orderItem.Product)
    .ToListAsync();

Console.WriteLine(JsonSerializer.Serialize(customers, new JsonSerializerOptions
{
    ReferenceHandler = ReferenceHandler.IgnoreCycles,
    WriteIndented = true
}));

var employees = await employeeRepository
    .Query()
    .ToListAsync();

Console.WriteLine(JsonSerializer.Serialize(employees, new JsonSerializerOptions
{
    ReferenceHandler = ReferenceHandler.IgnoreCycles,
    WriteIndented = true
}));