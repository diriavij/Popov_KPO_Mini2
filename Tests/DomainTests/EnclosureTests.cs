using Domain.Entities;

namespace Tests.DomainTests;

public class EnclosureTests
{
    [Fact]
    public void AddAnimal_WhenNotFull_AddsAndAnimalGetsMoved()
    {
        var enclosure = new Enclosure(EnclosureType.Herbivore, 50, 2);
        var animal = new Animal("Deer", "Bambi", DateTime.Now, Gender.Female, FoodType.Vegetables);

        enclosure.AddAnimal(animal);

        Assert.Contains(animal.Id, enclosure.Animals);
        Assert.Equal(enclosure.Id, animal.EnclosureId);
    }

    [Fact]
    public void AddAnimal_WhenFull_ThrowsInvalidOperationException()
    {
        var enclosure = new Enclosure(EnclosureType.Carnivore, 30, 1);
        var a1 = new Animal("Lion","Leo",DateTime.Now,Gender.Male,FoodType.Meat);
        var a2 = new Animal("Tiger","Tiggy",DateTime.Now,Gender.Female,FoodType.Meat);
        enclosure.AddAnimal(a1);
        Assert.Throws<InvalidOperationException>(() => enclosure.AddAnimal(a2));
    }

    [Fact]
    public void RemoveAnimal_WhenNotPresent_ThrowsInvalidOperationException()
    {
        var enclosure = new Enclosure(EnclosureType.Birds, 20, 2);
        var animal = new Animal("Eagle","Eddie",DateTime.Now,Gender.Male,FoodType.Meat);
        Assert.Throws<InvalidOperationException>(() => enclosure.RemoveAnimal(animal));
    }
}