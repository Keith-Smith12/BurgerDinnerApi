using Application.DTOs;

namespace Application.Services;

public interface IBurgerService
{
    Task<BurgerDto> GetByIdAsync(Guid id);
    Task<IReadOnlyList<BurgerDto>> GetAllAsync();
    Task<IReadOnlyList<BurgerDto>> GetAvailableAsync();
    Task<BurgerDto> CreateAsync(CreateBurgerDto createBurgerDto);
    Task<BurgerDto> UpdateAsync(Guid id, UpdateBurgerDto updateBurgerDto);
    Task DeleteAsync(Guid id);
}
