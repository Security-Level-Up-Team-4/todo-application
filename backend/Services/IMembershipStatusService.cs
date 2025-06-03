using backend.Models;
using backend.DTOs;

namespace backend.Services;

public interface IMembershipStatusService
{
    Task<IEnumerable<MembershipStatus>> GetAllStatusesAsync();
    Task<MembershipStatus?> GetStatusByIdAsync(int id);
    Task<MembershipStatus> CreateStatusAsync(MembershipStatusDto status);
    Task<MembershipStatus?> UpdateStatusAsync(int id, MembershipStatusDto updatedStatus);
    Task<bool> DeleteStatusAsync(int id);
}
