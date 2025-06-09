using backend.Models;
public interface ITimelineEventRepository
{
    Task AddAsync(TimelineEvent timelineEvent);
    Task<IEnumerable<TimelineEvent>> GetByTodoIdAsync(Guid todoId);
}
