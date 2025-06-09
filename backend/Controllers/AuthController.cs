using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using backend.DTOs;
using backend.Services;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            var result = await _authService.SignUpAsync(request);
            if (result == null)
                return BadRequest("Username or email already exists");

            return Ok(new
            {
                message = "User created successfully",
                result.Username,
                result.TotpSetupUri,
                result.TempSessionToken
            });
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.ValidateUserCredentialsAsync(request);
            if (user == null)
                return Unauthorized(new { message = "Invalid username or password." });


            var tempSessionToken = _authService.GenerateTempSessionToken(user.Id);

            if (user.Is2faEnabled)
            {
                return Ok(new
                {
                    requires2FA = true,
                    tempSessionToken
                });
            }
            
            return Ok(new 
            {
                message = "Two-factor authentication setup required",
                requires2FASetup = true,
                tempSetupToken,
                TotpSetupUri = $"otpauth://totp/TodoApp:{user.Username}?secret={user.TotpSecret}&issuer=TodoApp"
            });
        }
        
        [HttpPost("verify-2fa")]
        public async Task<IActionResult> Verify2FA([FromBody] Verify2FARequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.GetUserByTempSessionTokenAsync(request.TempSessionToken);
            if (user == null)
                return Unauthorized(new { message = "Invalid or expired session token." });

            var isValid = _authService.VerifyTotp(user, request.TotpCode);
            if (!isValid)
                return Unauthorized(new { message = "Invalid 2FA token." });

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }

        [HttpPost("confirm-2fa")]
        public async Task<IActionResult> ConfirmTwoFactor([FromBody] Confirm2FASetupRequest request)
        {
            var user = await _authService.GetUserByIdAsync(request.TempSessionUserId);
            if (user == null)
                return Unauthorized();

            var isValid = _authService.VerifyTotp(user, request.Token);
            if (!isValid)
                return BadRequest("Invalid TOTP code");

            await _authService.EnableTwoFactorAsync(user.Id);
            return Ok(new { message = "2FA enabled successfully" });
        }  
    }
}
