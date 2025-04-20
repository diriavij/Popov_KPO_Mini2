using Application.Services;
using Infrastructure.Repositories;
using Domain.Entities;

namespace Tests.ApplicationTests;

public class FeedingOrganizationServiceTests
{
    [Fact]
    public async Task ScheduleFeedingAsync_AddsSchedule()
    {
        var repo = new InMemoryFeedingScheduleRepository();
        var svc = new FeedingOrganizationService(repo);
        var animalId = Guid.NewGuid();
        var time = DateTime.Now;

        await svc.ScheduleFeedingAsync(animalId, time, FoodType.Meat);
        var list = await repo.ListAsync();
        Assert.Contains(list, s => s.AnimalId == animalId && s.Time == time);
    }

    [Fact]
    public async Task FeedNowAsync_MarksCompleted()
    {
        var repo = new InMemoryFeedingScheduleRepository();
        var svc = new FeedingOrganizationService(repo);
        var animalId = Guid.NewGuid();
        var sched = new FeedingSchedule(animalId, DateTime.Now, FoodType.Fish);
        await repo.AddAsync(sched);

        await svc.FeedNowAsync(sched.Id);
        var fetched = await repo.GetByIdAsync(sched.Id);
        Assert.True(fetched.IsCompleted);
    }
}