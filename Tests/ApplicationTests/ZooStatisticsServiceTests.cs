using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Tests.ApplicationTests;

public class ZooStatisticsServiceTests
{
    [Fact]
    public async Task GetStatisticsAsync_CalculatesCorrectly()
    {
        var animalRepo = new InMemoryAnimalRepository();
        var enclosureRepo = new InMemoryEnclosureRepository();
        // add animals
        var a1 = new Animal("Zebra","Zara",DateTime.Now,Gender.Female,FoodType.Vegetables);
        await animalRepo.AddAsync(a1);
        var enc1 = new Enclosure(EnclosureType.Herbivore,50,1);
        var enc2 = new Enclosure(EnclosureType.Aquarium,40,2);
        await enclosureRepo.AddAsync(enc1);
        await enclosureRepo.AddAsync(enc2);
        enc1.AddAnimal(a1);

        var svc = new ZooStatisticsService(animalRepo, enclosureRepo);
        var stats = await svc.GetStatisticsAsync();

        Assert.Equal(1, stats.TotalAnimals);
        Assert.Equal(2, stats.TotalEnclosures);
        Assert.Equal(1, stats.FreeEnclosures);
    }
}