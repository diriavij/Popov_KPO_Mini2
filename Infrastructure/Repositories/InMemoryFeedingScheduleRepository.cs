using Domain.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly Dictionary<Guid, FeedingSchedule> _store = new();
    public Task AddAsync(FeedingSchedule schedule) { _store[schedule.Id] = schedule; return Task.CompletedTask; }
    public Task<FeedingSchedule> GetByIdAsync(Guid id) => Task.FromResult(_store.GetValueOrDefault(id));
    public Task<IEnumerable<FeedingSchedule>> ListAsync() => Task.FromResult(_store.Values.AsEnumerable());
}
