using Domain.Entities;

namespace Domain.Interfaces;

public interface IEnclosureRepository
{
    Task<Enclosure> GetByIdAsync(Guid id);
    Task<IEnumerable<Enclosure>> ListAsync();
    Task AddAsync(Enclosure enclosure);
    Task RemoveAsync(Guid id);
}