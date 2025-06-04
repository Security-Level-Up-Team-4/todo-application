using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TaskStatusesRepository : ITaskStatusesRepository
{
    private readonly TodoContext _context;

    public TaskStatusesRepository(TodoContext context)
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

    public async Task<TaskStatuses> CreateAsync(TaskStatuses status)
    {
        _context.TaskStatuses.Add(status);
        await _context.SaveChangesAsync();
        return status;
    }

    public async Task UpdateAsync(TaskStatuses status)
    {
        _context.TaskStatuses.Update(status);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var status = await _context.TaskStatuses.FindAsync(id);
        if (status != null)
        {
            _context.TaskStatuses.Remove(status);
            await _context.SaveChangesAsync();
        }
    }
}
