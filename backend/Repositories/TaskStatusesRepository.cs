using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TaskStatusesRepository : ITaskStatusesRepository
{
    private readonly AppDbContext _context;

    public TaskStatusesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskStatuses>> GetAllAsync()
    {
        return await _context.TaskStatuses.ToListAsync();
    }

    public async Task<TaskStatuses?> GetByIdAsync(int id)
    {
        return await _context.TaskStatuses.FindAsync(id);
    }

    public Task<TaskStatuses?> GetByNameAsync(string name)
    {
        return _context.TaskStatuses
            .FirstOrDefaultAsync(ts => ts.name.ToLower().Equals(name.ToLower()));
    }
}
