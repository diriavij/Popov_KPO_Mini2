using Domain.Entities;

namespace Domain.Interfaces;

public interface IFeedingScheduleRepository
{
    Task<FeedingSchedule> GetByIdAsync(Guid id);
    Task<IEnumerable<FeedingSchedule>> ListAsync();
    Task AddAsync(FeedingSchedule schedule);
}