using Infrastructure.Repositories;
using Domain.Entities;

namespace Tests.InfrastructureTests;

public class InMemoryEnclosureRepositoryTests
{
    [Fact]
    public async Task AddAndGetAndList_ShouldPersist()
    {
        var repo = new InMemoryEnclosureRepository();
        var enc = new Enclosure(EnclosureType.Aquarium, 40, 5);
        await repo.AddAsync(enc);

        var fetched = await repo.GetByIdAsync(enc.Id);
        var all = await repo.ListAsync();

        Assert.NotNull(fetched);
        Assert.Equal(enc.Id, fetched.Id);
        Assert.Contains(all, e => e.Id == enc.Id);
    }

    [Fact]
    public async Task Remove_ShouldDelete()
    {
        var repo = new InMemoryEnclosureRepository();
        var enc = new Enclosure(EnclosureType.Birds, 20, 2);
        await repo.AddAsync(enc);
        await repo.RemoveAsync(enc.Id);

        var fetched = await repo.GetByIdAsync(enc.Id);
        Assert.Null(fetched);
    }
}