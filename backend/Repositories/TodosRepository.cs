using backend.Data;
using backend.Models;
using backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TodosRepository : ITodosRepository
{
    private readonly TodoContext _context;
    public readonly ITaskStatusesRepository _taskStatusesRepository;

    public TodosRepository(TodoContext context, ITaskStatusesRepository taskStatusesRepository)
    {
        _context = context;
        _taskStatusesRepository = taskStatusesRepository;
    }

    public async Task<List<Todos>> GetAllAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<Todos?> GetByIdAsync(Guid id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task AddAsync(Todos todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Todos todo)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
    }

    public Task<Todos?> GetByNameAsync(Guid teamId,string name)
    {
        var taskStatus = _taskStatusesRepository.GetByNameAsync("Open");
        return _context.Todos.FirstOrDefaultAsync(t => t.Title == name && t.TeamId == teamId && t.StatusId == taskStatus.Id);
    }
}
