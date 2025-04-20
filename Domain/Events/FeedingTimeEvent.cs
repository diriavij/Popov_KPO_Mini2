namespace Domain.Events;

public class FeedingTimeEvent
{
    public Guid ScheduleId { get; }
    public Guid AnimalId { get; }
    public DateTime ScheduledTime { get; }
    public DateTime OccurredAt { get; }

    public FeedingTimeEvent(Guid scheduleId, Guid animalId, DateTime scheduledTime)
    {
        ScheduleId = scheduleId;
        AnimalId = animalId;
        ScheduledTime = scheduledTime;
        OccurredAt = DateTime.UtcNow;
    }
}