using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class BurgerRepository : IBurgerRepository
{
    private static readonly List<Burger> _burgers = new();

    public Task<Burger?> GetByIdAsync(Guid id)
    {
        var burger = _burgers.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(burger);
    }

    public Task<IReadOnlyList<Burger>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Burger>>(_burgers.ToList());
    }

    public Task<IReadOnlyList<Burger>> GetAvailableAsync()
    {
        var availableBurgers = _burgers.Where(b => b.IsAvailable).ToList();
        return Task.FromResult<IReadOnlyList<Burger>>(availableBurgers);
    }

    public Task<Burger> CreateAsync(Burger burger)
    {
        _burgers.Add(burger);
        return Task.FromResult(burger);
    }

    public Task<Burger> UpdateAsync(Burger burger)
    {
        var existingBurger = _burgers.FirstOrDefault(b => b.Id == burger.Id);
        if (existingBurger != null)
        {
            _burgers.Remove(existingBurger);
            _burgers.Add(burger);
        }
        return Task.FromResult(burger);
    }

    public Task DeleteAsync(Guid id)
    {
        var burger = _burgers.FirstOrDefault(b => b.Id == id);
        if (burger != null)
        {
            _burgers.Remove(burger);
        }
        return Task.CompletedTask;
    }
}
