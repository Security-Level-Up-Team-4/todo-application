using backend.Models;

namespace backend.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string username, string email, string role);
        string GenerateTempSessionToken(string userId);
        JwtValidationResult? ValidateToken(string token);
    }
}
