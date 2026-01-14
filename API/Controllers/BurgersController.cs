using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BurgersController : ControllerBase
{
    private readonly IBurgerService _burgerService;

    public BurgersController(IBurgerService burgerService)
    {
        _burgerService = burgerService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BurgerDto>>> GetAll()
    {
        var burgers = await _burgerService.GetAllAsync();
        return Ok(burgers);
    }

    [HttpGet("available")]
    public async Task<ActionResult<IReadOnlyList<BurgerDto>>> GetAvailable()
    {
        var burgers = await _burgerService.GetAvailableAsync();
        return Ok(burgers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BurgerDto>> GetById(Guid id)
    {
        var burger = await _burgerService.GetByIdAsync(id);
        return Ok(burger);
    }

    [HttpPost]
    public async Task<ActionResult<BurgerDto>> Create(CreateBurgerDto createBurgerDto)
    {
        var burger = await _burgerService.CreateAsync(createBurgerDto);
        return CreatedAtAction(nameof(GetById), new { id = burger.Id }, burger);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<BurgerDto>> Update(Guid id, UpdateBurgerDto updateBurgerDto)
    {
        var burger = await _burgerService.UpdateAsync(id, updateBurgerDto);
        return Ok(burger);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _burgerService.DeleteAsync(id);
        return NoContent();
    }
}
