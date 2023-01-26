namespace Store.Data.Entities;

public class OrderItem : BaseEntity, IEntity
{
    public Guid OrderId { get; set; }

    public Order? Order { get; set; }

    public Guid ProductId { get; set; }

    public Product? Product { get; set; }

    public int Quantity { get; set; }

    public decimal? TotalPrice => Product?.Price * Quantity;
}