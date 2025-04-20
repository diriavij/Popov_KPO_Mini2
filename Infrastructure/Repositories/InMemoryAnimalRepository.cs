using Domain.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class InMemoryAnimalRepository : IAnimalRepository
{
    private readonly Dictionary<Guid, Animal> _store = new();
    public Task AddAsync(Animal animal)
    {
        _store[animal.Id] = animal;
        return Task.CompletedTask;
    }
    public Task<Animal> GetByIdAsync(Guid id) => Task.FromResult(_store.GetValueOrDefault(id));
    public Task<IEnumerable<Animal>> ListAsync() => Task.FromResult(_store.Values.AsEnumerable());
    public Task RemoveAsync(Guid id) { _store.Remove(id); return Task.CompletedTask; }
}