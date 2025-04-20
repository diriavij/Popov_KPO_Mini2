using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Tests.ApplicationTests;

public class AnimalTransferServiceTests
{
    [Fact]
    public async Task Transfer_WhenNoCurrentEnclosure_AddsToTarget()
    {
        var animalRepo = new InMemoryAnimalRepository();
        var enclosureRepo = new InMemoryEnclosureRepository();
        var animal = new Animal("Giraffe","Gemma",DateTime.Now,Gender.Female,FoodType.Vegetables);
        await animalRepo.AddAsync(animal);
        var enc = new Enclosure(EnclosureType.Herbivore,50,2);
        await enclosureRepo.AddAsync(enc);

        var svc = new AnimalTransferService(animalRepo, enclosureRepo);
        await svc.TransferAsync(animal.Id, enc.Id);

        var updatedEncl = await enclosureRepo.GetByIdAsync(enc.Id);
        Assert.Contains(animal.Id, updatedEncl.Animals);
    }

    [Fact]
    public async Task Transfer_WhenHasCurrentEnclosure_MovesProperly()
    {
        var animalRepo = new InMemoryAnimalRepository();
        var enclosureRepo = new InMemoryEnclosureRepository();
        var animal = new Animal("Bear","Boris",DateTime.Now,Gender.Male,FoodType.Fish);
        await animalRepo.AddAsync(animal);
        var enc1 = new Enclosure(EnclosureType.Carnivore,30,2);
        var enc2 = new Enclosure(EnclosureType.Carnivore,30,2);
        await enclosureRepo.AddAsync(enc1);
        await enclosureRepo.AddAsync(enc2);
        enc1.AddAnimal(animal);

        var svc = new AnimalTransferService(animalRepo, enclosureRepo);
        await svc.TransferAsync(animal.Id, enc2.Id);

        var list2 = (await enclosureRepo.ListAsync()).First(e => e.Id == enc2.Id);
        var list1 = (await enclosureRepo.ListAsync()).First(e => e.Id == enc1.Id);
        Assert.Contains(animal.Id, list2.Animals);
        Assert.DoesNotContain(animal.Id, list1.Animals);
    }
}