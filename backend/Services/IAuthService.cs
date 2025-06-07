using backend.DTOs;
using backend.Models;
using System;
using System.Threading.Tasks;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<SignUpResponse?> SignUpAsync(SignUpRequest request);
        Task<User?> ValidateUserCredentialsAsync(LoginRequest request);
        string GenerateTempSessionToken(Guid userId);
        Task<User?> GetUserByTempSessionTokenAsync(string tempSessionToken);
        bool VerifyTotp(User user, string totpCode);
        string GenerateJwtToken(User user);
        Task<string?> SetupTwoFactorAsync(Guid userId);
        Task EnableTwoFactorAsync(Guid userId);
        Task<User?> GetUserByIdAsync(Guid userId);
    }
}
