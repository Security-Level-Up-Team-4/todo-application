using backend.Models;
namespace backend.Repositories;

public interface IMembershipStatusRepository
{
    Task<IEnumerable<MembershipStatus>> GetAllAsync();
    Task<MembershipStatus?> GetByIdAsync(int id);
    Task<MembershipStatus> AddAsync(MembershipStatus status);
    Task UpdateAsync(MembershipStatus status);
    Task<MembershipStatus?> GetByNameAsync(string name);

}
