using backend.Models;
namespace backend.Repositories;

public interface IMembershipStatusRepository
{
    Task<IEnumerable<MembershipStatus>> GetAllAsync();
    Task<MembershipStatus?> GetByIdAsync(int id);
    Task<MembershipStatus?> GetByNameAsync(string name);

}
