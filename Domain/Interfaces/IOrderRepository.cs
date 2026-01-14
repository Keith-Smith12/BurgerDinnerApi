using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Order>> GetAllAsync();
    Task<IReadOnlyList<Order>> GetByStatusAsync(OrderStatus status);
    Task<Order> CreateAsync(Order order);
    Task<Order> UpdateAsync(Order order);
    Task DeleteAsync(Guid id);
}
