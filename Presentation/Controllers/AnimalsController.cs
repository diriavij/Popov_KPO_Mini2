using Microsoft.AspNetCore.Mvc;
using Application;
using Domain.Entities;
using Domain.Interfaces;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _repo;
    private readonly IAnimalTransferService _transfer;
    public AnimalsController(IAnimalRepository repo, IAnimalTransferService transfer)
    {
        _repo = repo;
        _transfer = transfer;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.ListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var a = await _repo.GetByIdAsync(id);
        return a == null ? NotFound() : Ok(a);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AnimalCreateDto dto)
    {
        var animal = new Animal(dto.Species, dto.Name, dto.BirthDate, dto.Gender, dto.FavoriteFood);
        await _repo.AddAsync(animal);
        return CreatedAtAction(nameof(Get), new { id = animal.Id }, animal);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repo.RemoveAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/transfer/{enclosureId}")]
    public async Task<IActionResult> Transfer(Guid id, Guid enclosureId)
    {
        await _transfer.TransferAsync(id, enclosureId);
        return NoContent();
    }
}

// DTOs
public record AnimalCreateDto(string Species, string Name, DateTime BirthDate, Gender Gender, FoodType FavoriteFood);
