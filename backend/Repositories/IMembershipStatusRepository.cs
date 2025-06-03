using backend.Models;
namespace backend.Repositories;

public interface IMembershipStatusRepository
{
    Task<IEnumerable<MembershipStatus>> GetAllAsync();
}
