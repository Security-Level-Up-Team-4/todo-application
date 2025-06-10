using backend.Models;
using backend.DTOs;

namespace backend.Services;

public interface IMembershipStatusService
{
    Task<IEnumerable<MembershipStatus>> GetAllStatusesAsync();
    Task<MembershipStatus?> GetStatusByIdAsync(int id);
    
}
