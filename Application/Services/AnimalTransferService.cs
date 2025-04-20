using Domain.Interfaces;

namespace Application.Services;

public class AnimalTransferService : IAnimalTransferService
{
    private readonly IAnimalRepository _animals;
    private readonly IEnclosureRepository _enclosures;

    public AnimalTransferService(IAnimalRepository animals, IEnclosureRepository enclosures)
    {
        _animals = animals;
        _enclosures = enclosures;
    }

    public async Task TransferAsync(Guid animalId, Guid targetEnclosureId)
    {
        var animal = await _animals.GetByIdAsync(animalId);
        var target = await _enclosures.GetByIdAsync(targetEnclosureId);
        var currentEnclosureId = animal.EnclosureId;
        if (currentEnclosureId.HasValue)
        {
            var current = await _enclosures.GetByIdAsync(currentEnclosureId.Value);
            current.RemoveAnimal(animal);
        }
        target.AddAnimal(animal);
    }
}