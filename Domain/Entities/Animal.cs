namespace Domain.Entities;

public enum Gender { Male, Female }
public enum HealthStatus { Healthy, Sick }
public enum FoodType { Meat, Vegetables, Fish }

public class Animal
{
    public Guid Id { get; private set; }
    public string Species { get; private set; }
    public string Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public FoodType FavoriteFood { get; private set; }
    public HealthStatus Status { get; private set; }
    public Guid? EnclosureId { get; private set; }

    public event Action<Events.AnimalMovedEvent> AnimalMoved;

    public Animal(string species, string name, DateTime birthDate, Gender gender, FoodType favoriteFood)
    {
        Id = Guid.NewGuid();
        Species = species;
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
        FavoriteFood = favoriteFood;
        Status = HealthStatus.Healthy;
    }

    public void Feed()
    {
        if (Status == HealthStatus.Sick)
            throw new InvalidOperationException("Cannot feed a sick animal.");
    }

    public void Heal()
    {
        Status = HealthStatus.Healthy;
    }

    public void MoveTo(Guid newEnclosureId)
    {
        var old = EnclosureId;
        EnclosureId = newEnclosureId;
        AnimalMoved?.Invoke(new Events.AnimalMovedEvent(this.Id, old, newEnclosureId));
    }
}