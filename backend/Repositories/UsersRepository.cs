using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly TodoContext _context;

    public UsersRepository(TodoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Users>> GetAllAsync() =>
        await _context.Users.ToListAsync();

    public async Task<Users?> GetByIdAsync(Guid id) =>
        await _context.Users.FindAsync(id);

    public async Task<Users?> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task AddAsync(Users user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Users user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
