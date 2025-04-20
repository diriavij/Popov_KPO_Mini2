namespace Domain.Entities;

public class FeedingSchedule
{
    public Guid Id { get; private set; }
    public Guid AnimalId { get; private set; }
    public DateTime Time { get; private set; }
    public FoodType FoodType { get; private set; }
    public bool IsCompleted { get; private set; }

    public event Action<Events.FeedingTimeEvent> FeedingTime;

    public FeedingSchedule(Guid animalId, DateTime time, FoodType foodType)
    {
        Id = Guid.NewGuid();
        AnimalId = animalId;
        Time = time;
        FoodType = foodType;
        IsCompleted = false;
    }

    public void Reschedule(DateTime newTime)
    {
        Time = newTime;
    }

    public void MarkCompleted()
    {
        IsCompleted = true;
        FeedingTime?.Invoke(new Events.FeedingTimeEvent(this.Id, AnimalId, Time));
    }
}