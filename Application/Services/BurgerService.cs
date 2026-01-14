using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class BurgerService : IBurgerService
{
    private readonly IBurgerRepository _burgerRepository;
    private readonly IIngredientRepository _ingredientRepository;

    public BurgerService(IBurgerRepository burgerRepository, IIngredientRepository ingredientRepository)
    {
        _burgerRepository = burgerRepository;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<BurgerDto> GetByIdAsync(Guid id)
    {
        var burger = await _burgerRepository.GetByIdAsync(id);
        if (burger == null)
            throw new KeyNotFoundException($"Burger with id {id} not found");

        return MapToDto(burger);
    }

    public async Task<IReadOnlyList<BurgerDto>> GetAllAsync()
    {
        var burgers = await _burgerRepository.GetAllAsync();
        return burgers.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<BurgerDto>> GetAvailableAsync()
    {
        var burgers = await _burgerRepository.GetAvailableAsync();
        return burgers.Select(MapToDto).ToList();
    }

    public async Task<BurgerDto> CreateAsync(CreateBurgerDto createBurgerDto)
    {
        var burger = new Burger(
            createBurgerDto.Name,
            createBurgerDto.Description,
            createBurgerDto.Price
        );

        foreach (var ingredientId in createBurgerDto.IngredientIds)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(ingredientId);
            if (ingredient != null)
            {
                burger.AddIngredient(ingredient, 1);
            }
        }

        var createdBurger = await _burgerRepository.CreateAsync(burger);
        return MapToDto(createdBurger);
    }

    public async Task<BurgerDto> UpdateAsync(Guid id, UpdateBurgerDto updateBurgerDto)
    {
        var burger = await _burgerRepository.GetByIdAsync(id);
        if (burger == null)
            throw new KeyNotFoundException($"Burger with id {id} not found");

        if (updateBurgerDto.Name != null)
            burger.UpdateName(updateBurgerDto.Name);

        if (updateBurgerDto.Description != null)
            burger.UpdateDescription(updateBurgerDto.Description);

        if (updateBurgerDto.Price.HasValue)
            burger.UpdatePrice(updateBurgerDto.Price.Value);

        if (updateBurgerDto.IsAvailable.HasValue)
            burger.SetAvailability(updateBurgerDto.IsAvailable.Value);

        var updatedBurger = await _burgerRepository.UpdateAsync(burger);
        return MapToDto(updatedBurger);
    }

    public async Task DeleteAsync(Guid id)
    {
        var burger = await _burgerRepository.GetByIdAsync(id);
        if (burger == null)
            throw new KeyNotFoundException($"Burger with id {id} not found");

        await _burgerRepository.DeleteAsync(id);
    }

    private static BurgerDto MapToDto(Burger burger)
    {
        return new BurgerDto
        {
            Id = burger.Id,
            Name = burger.Name,
            Description = burger.Description,
            Price = burger.Price,
            IsAvailable = burger.IsAvailable,
            CreatedAt = burger.CreatedAt,
            UpdatedAt = burger.UpdatedAt,
            Ingredients = burger.Ingredients.Select(bi => new IngredientDto
            {
                Id = bi.Ingredient.Id,
                Name = bi.Ingredient.Name,
                Description = bi.Ingredient.Description,
                StockQuantity = bi.Ingredient.StockQuantity,
                Unit = bi.Ingredient.Unit,
                CreatedAt = bi.Ingredient.CreatedAt,
                UpdatedAt = bi.Ingredient.UpdatedAt
            }).ToList()
        };
    }
}
