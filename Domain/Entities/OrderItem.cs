namespace Domain.Entities;

public class OrderItem
{
    public Guid OrderId { get; private set; }
    public Guid BurgerId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public Order Order { get; private set; } = null!;
    public Burger Burger { get; private set; } = null!;

    public OrderItem(Guid orderId, Guid burgerId, int quantity, decimal unitPrice)
    {
        OrderId = orderId;
        BurgerId = burgerId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));
        
        Quantity = quantity;
    }
}
