using Microsoft.AspNetCore.Mvc;
using Application;
using Domain.Interfaces;
using Domain.Entities;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedingController : ControllerBase
{
    private readonly IFeedingScheduleRepository _repo;
    private readonly IFeedingOrganizationService _service;

    public FeedingController(IFeedingScheduleRepository repo, IFeedingOrganizationService service)
    {
        _repo = repo;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repo.ListAsync());

    [HttpPost]
    public async Task<IActionResult> Schedule([FromBody] FeedingCreateDto dto)
    {
        await _service.ScheduleFeedingAsync(dto.AnimalId, dto.Time, dto.FoodType);
        return Ok();
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        await _service.FeedNowAsync(id);
        return NoContent();
    }
}

public record FeedingCreateDto(Guid AnimalId, DateTime Time, FoodType FoodType);
