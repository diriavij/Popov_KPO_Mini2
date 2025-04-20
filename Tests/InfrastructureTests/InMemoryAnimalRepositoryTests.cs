using Infrastructure.Repositories;
using Domain.Entities;

namespace Tests.InfrastructureTests;

public class InMemoryAnimalRepositoryTests
{
    [Fact]
    public async Task AddAndGetAndList_ShouldPersist()
    {
        var repo = new InMemoryAnimalRepository();
        var animal = new Animal("Wolf","Wally",DateTime.Now,Gender.Male,FoodType.Meat);
        await repo.AddAsync(animal);

        var fetched = await repo.GetByIdAsync(animal.Id);
        var all = await repo.ListAsync();

        Assert.NotNull(fetched);
        Assert.Equal(animal.Id, fetched.Id);
        Assert.Contains(all, a => a.Id == animal.Id);
    }

    [Fact]
    public async Task Remove_ShouldDelete()
    {
        var repo = new InMemoryAnimalRepository();
        var animal = new Animal("Fox","Fiona",DateTime.Now,Gender.Female,FoodType.Meat);
        await repo.AddAsync(animal);
        await repo.RemoveAsync(animal.Id);

        var fetched = await repo.GetByIdAsync(animal.Id);
        Assert.Null(fetched);
    }
}