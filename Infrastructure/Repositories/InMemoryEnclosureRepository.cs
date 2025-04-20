using Domain.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class InMemoryEnclosureRepository : IEnclosureRepository
{
    private readonly Dictionary<Guid, Enclosure> _store = new();
    public Task AddAsync(Enclosure enclosure) { _store[enclosure.Id] = enclosure; return Task.CompletedTask; }
    public Task<Enclosure> GetByIdAsync(Guid id) => Task.FromResult(_store.GetValueOrDefault(id));
    public Task<IEnumerable<Enclosure>> ListAsync() => Task.FromResult(_store.Values.AsEnumerable());
    public Task RemoveAsync(Guid id) { _store.Remove(id); return Task.CompletedTask; }
}