using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class IngredientRepository : IIngredientRepository
{
    private static readonly List<Ingredient> _ingredients = new();

    public Task<Ingredient?> GetByIdAsync(Guid id)
    {
        var ingredient = _ingredients.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(ingredient);
    }

    public Task<IReadOnlyList<Ingredient>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Ingredient>>(_ingredients.ToList());
    }

    public Task<Ingredient> CreateAsync(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
        return Task.FromResult(ingredient);
    }

    public Task<Ingredient> UpdateAsync(Ingredient ingredient)
    {
        var existingIngredient = _ingredients.FirstOrDefault(i => i.Id == ingredient.Id);
        if (existingIngredient != null)
        {
            _ingredients.Remove(existingIngredient);
            _ingredients.Add(ingredient);
        }
        return Task.FromResult(ingredient);
    }

    public Task DeleteAsync(Guid id)
    {
        var ingredient = _ingredients.FirstOrDefault(i => i.Id == id);
        if (ingredient != null)
        {
            _ingredients.Remove(ingredient);
        }
        return Task.CompletedTask;
    }
}
