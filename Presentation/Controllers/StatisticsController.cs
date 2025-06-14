using Microsoft.AspNetCore.Mvc;
using Application;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IZooStatisticsService _stats;
    public StatisticsController(IZooStatisticsService stats) => _stats = stats;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _stats.GetStatisticsAsync());
}