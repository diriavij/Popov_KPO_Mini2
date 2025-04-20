using Domain.Entities;

namespace Application;

public interface IAnimalTransferService
{
    Task TransferAsync(Guid animalId, Guid targetEnclosureId);
}

public interface IFeedingOrganizationService
{
    Task ScheduleFeedingAsync(Guid animalId, DateTime time, FoodType type);
    Task FeedNowAsync(Guid scheduleId);
}

public interface IZooStatisticsService
{
    Task<ZooStatsDto> GetStatisticsAsync();
}

public class ZooStatsDto
{
    public int TotalAnimals { get; set; }
    public int TotalEnclosures { get; set; }
    public int FreeEnclosures { get; set; }
}