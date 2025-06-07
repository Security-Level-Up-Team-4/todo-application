namespace backend.DTOs
{
    public class SignUpRequest
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class Verify2FARequest
    {
        public string TempSessionToken { get; set; } = string.Empty;
        public string TotpCode { get; set; } = string.Empty;
    }

    public class Confirm2FASetupRequest
    {
        public Guid TempSessionUserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }

    public class SignUpResponse
    {
        public string Username { get; set; } = string.Empty;
        public string TotpSetupUri { get; set; } = string.Empty;
        public string TempSessionToken { get; set; } = string.Empty;
    }

}
