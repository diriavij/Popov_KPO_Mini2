using Domain.Entities;
using Domain.Events;

namespace Tests.DomainTests;

public class FeedingScheduleTests
{
    [Fact]
    public void Reschedule_UpdatesTime()
    {
        var original = new DateTime(2025,4,25,9,0,0);
        var schedule = new FeedingSchedule(Guid.NewGuid(), original, FoodType.Fish);
        var newTime = original.AddHours(1);
        schedule.Reschedule(newTime);
        Assert.Equal(newTime, schedule.Time);
    }

    [Fact]
    public void MarkCompleted_SetsIsCompleted_And_RaisesEvent()
    {
        var schedule = new FeedingSchedule(Guid.NewGuid(), DateTime.Now, FoodType.Meat);
        FeedingTimeEvent received = null;
        schedule.FeedingTime += e => received = e;

        schedule.MarkCompleted();

        Assert.True(schedule.IsCompleted);
        Assert.NotNull(received);
        Assert.Equal(schedule.Id, received.ScheduleId);
        Assert.Equal(schedule.AnimalId, received.AnimalId);
        Assert.Equal(schedule.Time, received.ScheduledTime);
    }
}