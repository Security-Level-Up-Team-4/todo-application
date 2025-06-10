using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TimelineEventRepository : ITimelineEventRepository
{
    private readonly AppDbContext _context;

    public TimelineEventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TimelineEvent>> GetByTodoIdAsync(Guid todoId)
    {
        return await _context.TimelineEvents
            .Where(te => te.TodoId == todoId)
            .OrderBy(te => te.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(TimelineEvent timelineEvent)
    {
        await _context.TimelineEvents.AddAsync(timelineEvent);
        await _context.SaveChangesAsync();
    }
}
