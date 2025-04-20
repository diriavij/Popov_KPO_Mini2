using Domain.Entities;
using Domain.Events;

namespace Tests.DomainTests;

public class AnimalTests
{
    [Fact]
    public void Feed_WhenHealthy_DoesNotThrow()
    {
        var animal = new Animal("Lion", "Simba", new DateTime(2020,1,1), Gender.Male, FoodType.Meat);
        animal.Feed();
    }

    [Fact]
    public void Feed_WhenSick_ThrowsInvalidOperationException()
    {
        var animal = new Animal("Tiger", "Tiggy", DateTime.Now, Gender.Female, FoodType.Meat);
        animal.Heal();
        typeof(Animal).GetProperty("Status").SetValue(animal, HealthStatus.Sick);
        Assert.Throws<InvalidOperationException>(() => animal.Feed());
    }

    [Fact]
    public void MoveTo_ChangesEnclosureId_And_RaisesEvent()
    {
        var animal = new Animal("Bear", "Baloo", DateTime.Now, Gender.Male, FoodType.Fish);
        Guid? oldEnclosure = animal.EnclosureId;
        Guid newId = Guid.NewGuid();

        AnimalMovedEvent received = null;
        animal.AnimalMoved += e => received = e;

        animal.MoveTo(newId);

        Assert.Equal(newId, animal.EnclosureId);
        Assert.NotNull(received);
        Assert.Equal(animal.Id, received.AnimalId);
        Assert.Equal(oldEnclosure, received.OldEnclosureId);
        Assert.Equal(newId, received.NewEnclosureId);
    }
}