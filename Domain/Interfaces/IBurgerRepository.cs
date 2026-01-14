using Domain.Entities;

namespace Domain.Interfaces;

public interface IBurgerRepository
{
    Task<Burger?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Burger>> GetAllAsync();
    Task<IReadOnlyList<Burger>> GetAvailableAsync();
    Task<Burger> CreateAsync(Burger burger);
    Task<Burger> UpdateAsync(Burger burger);
    Task DeleteAsync(Guid id);
}
