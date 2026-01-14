using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private static readonly List<Order> _orders = new();

    public Task<Order?> GetByIdAsync(Guid id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        return Task.FromResult(order);
    }

    public Task<IReadOnlyList<Order>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Order>>(_orders.ToList());
    }

    public Task<IReadOnlyList<Order>> GetByStatusAsync(OrderStatus status)
    {
        var ordersByStatus = _orders.Where(o => o.Status == status).ToList();
        return Task.FromResult<IReadOnlyList<Order>>(ordersByStatus);
    }

    public Task<Order> CreateAsync(Order order)
    {
        _orders.Add(order);
        return Task.FromResult(order);
    }

    public Task<Order> UpdateAsync(Order order)
    {
        var existingOrder = _orders.FirstOrDefault(o => o.Id == order.Id);
        if (existingOrder != null)
        {
            _orders.Remove(existingOrder);
            _orders.Add(order);
        }
        return Task.FromResult(order);
    }

    public Task DeleteAsync(Guid id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            _orders.Remove(order);
        }
        return Task.CompletedTask;
    }
}
