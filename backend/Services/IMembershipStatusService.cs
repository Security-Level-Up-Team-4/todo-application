using backend.Models;
using backend.DTOs;

namespace backend.Services;

public interface IMembershipStatusService
{
    Task<IEnumerable<MembershipStatus>> GetAllStatusesAsync();
    Task<MembershipStatus> CreateStatusAsync(MembershipStatusDto status);//Do we need this
    Task<MembershipStatus?> GetStatusByIdAsync(int id);
    Task<MembershipStatus?> UpdateStatusAsync(int id, MembershipStatusDto updatedStatus);//Do we need this
    
}
