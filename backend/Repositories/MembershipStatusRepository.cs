using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;
namespace backend.Repositories;
public class MembershipStatusRepository : IMembershipStatusRepository
{
    private readonly TodoContext _context;
    public MembershipStatusRepository(TodoContext context) => _context = context;

    public async Task<IEnumerable<MembershipStatus>> GetAllAsync() =>
        await _context.MembershipStatuses.ToListAsync();
}
