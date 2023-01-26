namespace Store.Data.Entities;

public class Product : BaseEntity, IEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }
}