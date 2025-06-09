using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;
namespace backend.Repositories;

public class MembershipStatusRepository : IMembershipStatusRepository
{
    private readonly AppDbContext _context;
    public MembershipStatusRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<MembershipStatus>> GetAllAsync() =>
        await _context.MembershipStatuses.ToListAsync();
    public async Task<MembershipStatus?> GetByIdAsync(int id) =>
        await _context.MembershipStatuses.FindAsync(id);

    public async Task<MembershipStatus?> GetByNameAsync(string name)
    {
        return await _context.MembershipStatuses
            .FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
    }
}
