namespace Store.Data.Entities;

public class Order : BaseEntity, IEntity
{
    public Guid CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public IEnumerable<OrderItem>? OrderItems { get; set; }

    public decimal? Total => OrderItems?.Select(orderItem => orderItem.TotalPrice).Sum();
}