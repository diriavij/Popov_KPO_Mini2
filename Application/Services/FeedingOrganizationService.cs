using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class FeedingOrganizationService : IFeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _schedules;

    public FeedingOrganizationService(IFeedingScheduleRepository schedules)
    {
        _schedules = schedules;
    }

    public async Task ScheduleFeedingAsync(Guid animalId, DateTime time, FoodType type)
    {
        var schedule = new Domain.Entities.FeedingSchedule(animalId, time, type);
        await _schedules.AddAsync(schedule);
    }

    public async Task FeedNowAsync(Guid scheduleId)
    {
        var schedule = await _schedules.GetByIdAsync(scheduleId);
        schedule.MarkCompleted();
    }
}