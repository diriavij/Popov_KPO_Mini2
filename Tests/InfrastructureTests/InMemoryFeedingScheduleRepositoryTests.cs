using Infrastructure.Repositories;
using Domain.Entities;

namespace Tests.InfrastructureTests;

public class InMemoryFeedingScheduleRepositoryTests
{
    [Fact]
    public async Task AddAndGetAndList_ShouldPersist()
    {
        var repo = new InMemoryFeedingScheduleRepository();
        var sched = new FeedingSchedule(Guid.NewGuid(), DateTime.Now, FoodType.Vegetables);
        await repo.AddAsync(sched);

        var fetched = await repo.GetByIdAsync(sched.Id);
        var all = await repo.ListAsync();

        Assert.NotNull(fetched);
        Assert.Equal(sched.Id, fetched.Id);
        Assert.Contains(all, s => s.Id == sched.Id);
    }
}