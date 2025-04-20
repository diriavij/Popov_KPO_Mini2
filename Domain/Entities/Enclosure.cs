namespace Domain.Entities;
public enum EnclosureType { Carnivore, Herbivore, Birds, Aquarium }

public class Enclosure
{
    public Guid Id { get; private set; }
    public EnclosureType Type { get; private set; }
    public int Size { get; private set; }
    public int Capacity { get; private set; }
    private readonly List<Guid> _animals = new List<Guid>();
    public IReadOnlyCollection<Guid> Animals => _animals.AsReadOnly();

    public Enclosure(EnclosureType type, int size, int capacity)
    {
        Id = Guid.NewGuid();
        Type = type;
        Size = size;
        Capacity = capacity;
    }

    public void AddAnimal(Animal animal)
    {
        if (_animals.Count >= Capacity)
            throw new InvalidOperationException("Enclosure is full.");
        _animals.Add(animal.Id);
        animal.MoveTo(this.Id);
    }

    public void RemoveAnimal(Animal animal)
    {
        if (!_animals.Remove(animal.Id))
            throw new InvalidOperationException("Animal not found in enclosure.");
    }
}