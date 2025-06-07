using backend.DTOs;
using backend.Models;
using backend.Repositories;
using System;
using System.Threading.Tasks;
using BCrypt.Net;
using OtpNet;
using System.Security.Claims;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;

        public AuthService(IAuthRepository authRepository, IJwtService jwtService)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
        }

        public async Task<SignUpResponse?> SignUpAsync(SignUpRequest request)
        {
            if (await _authRepository.GetByUsernameAsync(request.Username) != null ||
                await _authRepository.GetByEmailAsync(request.Email) != null)
            {
                return null;
            }

            var totpSecret = KeyGeneration.GenerateRandomKey(20);
            var totpSecretBase32 = Base32Encoding.ToString(totpSecret);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                TotpSecret = totpSecretBase32,
                RoleId = 3, // todo_user role id
                Is2faEnabled = false,
                IsLocked = false,
                FailedAttempts = 0,
                CreatedAt = DateTime.UtcNow
            };

            await _authRepository.AddUserAsync(user);
            await _authRepository.SaveChangesAsync();

            var tempToken = _jwtService.GenerateTempSessionToken(user.Id.ToString());

            return new SignUpResponse
            {
                Username = user.Username,
                TotpSetupUri = $"otpauth://totp/TodoApp:{user.Username}?secret={user.TotpSecret}&issuer=TodoApp",
                TempSessionToken = tempToken
            };
        }

        // Matches controller's ValidateUserCredentialsAsync(LoginRequest)
        public async Task<User?> ValidateUserCredentialsAsync(LoginRequest request)
        {
            var user = await _authRepository.GetByUsernameAsync(request.Username);
            if (user == null || user.IsLocked)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                user.FailedAttempts++;
                if (user.FailedAttempts >= 5) user.IsLocked = true;
                await _authRepository.SaveChangesAsync();
                return null;
            }

            user.FailedAttempts = 0;
            await _authRepository.SaveChangesAsync();

            return user;
        }

        // Generates a temp session token (for 2FA)
        public string GenerateTempSessionToken(Guid userId)
        {
            return _jwtService.GenerateTempSessionToken(userId.ToString());
        }

        // Gets user from temp session token, for 2FA verification
        public async Task<User?> GetUserByTempSessionTokenAsync(string tempSessionToken)
        {
            var validationResult = _jwtService.ValidateToken(tempSessionToken);
            if (validationResult == null || validationResult.ValidTo < DateTime.UtcNow)
                return null;

            var userIdClaim = validationResult.Principal.FindFirst("sub");
            if (userIdClaim == null)
                return null;

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
                return null;

            var user = await _authRepository.GetByIdAsync(userId);
            return user;
        }


        public bool VerifyTotp(User user, string totpCode)
        {
            var totp = new Totp(Base32Encoding.ToBytes(user.TotpSecret));
            return totp.VerifyTotp(totpCode, out _, VerificationWindow.RfcSpecifiedNetworkDelay);
        }

        public string GenerateJwtToken(User user)
        {
            return _jwtService.GenerateToken(user.Id.ToString(), user.Username, user.RoleId);
        }

        public async Task<string?> SetupTwoFactorAsync(Guid userId)
        {
            var user = await _authRepository.GetByIdAsync(userId);
            if (user == null) return null;

            var totpSecret = KeyGeneration.GenerateRandomKey(20);
            var totpSecretBase32 = Base32Encoding.ToString(totpSecret);

            user.TotpSecret = totpSecretBase32;
            await _authRepository.SaveChangesAsync();

            return $"otpauth://totp/TodoApp:{user.Username}?secret={user.TotpSecret}&issuer=TodoApp";
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _authRepository.GetByIdAsync(userId);
        }

        public async Task EnableTwoFactorAsync(Guid userId)
        {
            var user = await _authRepository.GetByIdAsync(userId);
            if (user == null) return;

            user.Is2faEnabled = true;
            await _authRepository.SaveChangesAsync();
        }
    }
}
