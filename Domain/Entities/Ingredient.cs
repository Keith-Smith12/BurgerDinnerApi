namespace Domain.Entities;

public class Ingredient
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal StockQuantity { get; private set; }
    public string Unit { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Ingredient(string name, string description, decimal stockQuantity, string unit)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        StockQuantity = stockQuantity;
        Unit = unit;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateStock(decimal quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Stock quantity cannot be negative", nameof(quantity));
        
        StockQuantity = quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddToStock(decimal quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));
        
        StockQuantity += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveFromStock(decimal quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));
        
        if (StockQuantity < quantity)
            throw new InvalidOperationException("Insufficient stock quantity");
        
        StockQuantity -= quantity;
        UpdatedAt = DateTime.UtcNow;
    }
}
