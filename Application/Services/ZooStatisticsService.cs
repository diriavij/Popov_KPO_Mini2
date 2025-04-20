using Domain.Interfaces;

namespace Application.Services;

public class ZooStatisticsService : IZooStatisticsService
{
    private readonly IAnimalRepository _animals;
    private readonly IEnclosureRepository _enclosures;

    public ZooStatisticsService(IAnimalRepository animals, IEnclosureRepository enclosures)
    {
        _animals = animals;
        _enclosures = enclosures;
    }

    public async Task<ZooStatsDto> GetStatisticsAsync()
    {
        var allAnimals = await _animals.ListAsync();
        var all = await _enclosures.ListAsync();
        var free = all.Count(e => e.Capacity > e.Animals.Count);
        return new ZooStatsDto
        {
            TotalAnimals = allAnimals.Count(),
            TotalEnclosures = all.Count(),
            FreeEnclosures = free
        };
    }
}