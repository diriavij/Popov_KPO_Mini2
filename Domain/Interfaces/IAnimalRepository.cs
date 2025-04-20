using Domain.Entities;

namespace Domain.Interfaces;

public interface IAnimalRepository
{
    Task<Animal> GetByIdAsync(Guid id);
    Task<IEnumerable<Animal>> ListAsync();
    Task AddAsync(Animal animal);
    Task RemoveAsync(Guid id);
}