using Domain.Entities;

namespace Domain.Interfaces;

public interface IIngredientRepository
{
    Task<Ingredient?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Ingredient>> GetAllAsync();
    Task<Ingredient> CreateAsync(Ingredient ingredient);
    Task<Ingredient> UpdateAsync(Ingredient ingredient);
    Task DeleteAsync(Guid id);
}
