using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TodosRepository : ITodosRepository
{
    private readonly TodoContext _context;

    public TodosRepository(TodoContext context)
    {
        _context = context;
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

    public async Task DeleteAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo != null)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }
}
