using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class MembershipStatusService : IMembershipStatusService
{
    private readonly TodoContext _context;

    public MembershipStatusService(TodoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MembershipStatus>> GetAllStatusesAsync()
    {
        return await _context.MembershipStatuses.ToListAsync();
    }

    public async Task<MembershipStatus?> GetStatusByIdAsync(int id)
    {
        return await _context.MembershipStatuses.FindAsync(id);
    }

    public async Task<MembershipStatus> CreateStatusAsync(MembershipStatusDto status)
    {
        var newStatus = new MembershipStatus
        {
            Name = status.Name
        };
        _context.MembershipStatuses.Add(newStatus);
        await _context.SaveChangesAsync();
        return newStatus;
    }

    public async Task<MembershipStatus?> UpdateStatusAsync(int id, MembershipStatusDto updatedStatus)
    {
        var existing = await _context.MembershipStatuses.FindAsync(id);
        if (existing == null) return null;

        existing.Name = updatedStatus.Name;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteStatusAsync(int id)
    {
        var status = await _context.MembershipStatuses.FindAsync(id);
        if (status == null) return false;

        _context.MembershipStatuses.Remove(status);
        await _context.SaveChangesAsync();
        return true;
    }
}
