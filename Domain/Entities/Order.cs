using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public string CustomerName { get; private set; }
    public string CustomerEmail { get; private set; }
    public string CustomerPhone { get; private set; }
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Order(string customerName, string customerEmail, string customerPhone)
    {
        Id = Guid.NewGuid();
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(Burger burger, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));

        var existingItem = _items.FirstOrDefault(i => i.BurgerId == burger.Id);
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(quantity);
        }
        else
        {
            _items.Add(new OrderItem(Id, burger.Id, quantity, unitPrice));
        }

        CalculateTotal();
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveItem(Guid burgerId)
    {
        var item = _items.FirstOrDefault(i => i.BurgerId == burgerId);
        if (item != null)
        {
            _items.Remove(item);
            CalculateTotal();
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    private void CalculateTotal()
    {
        TotalAmount = _items.Sum(item => item.Quantity * item.UnitPrice);
    }
}
