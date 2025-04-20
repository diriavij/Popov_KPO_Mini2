using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Interfaces;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly IEnclosureRepository _repo;
    public EnclosuresController(IEnclosureRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.ListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var e = await _repo.GetByIdAsync(id);
        return e == null ? NotFound() : Ok(e);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EnclosureCreateDto dto)
    {
        var enc = new Enclosure(dto.Type, dto.Size, dto.Capacity);
        await _repo.AddAsync(enc);
        return CreatedAtAction(nameof(Get), new { id = enc.Id }, enc);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repo.RemoveAsync(id);
        return NoContent();
    }
}

public record EnclosureCreateDto(EnclosureType Type, int Size, int Capacity);
