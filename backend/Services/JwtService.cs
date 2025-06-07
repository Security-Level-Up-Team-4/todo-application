using Microsoft.IdentityModel.Tokens;
using backend.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiryMinutes;

        public JwtService()
        {
            _secretKey = Environment.GetEnvironmentVariable("JWT_KEY") 
                         ?? throw new ArgumentNullException("JWT_SECRET_KEY environment variable is missing");

            _issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") 
                      ?? throw new ArgumentNullException("JWT_ISSUER environment variable is missing");

            _audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") 
                        ?? throw new ArgumentNullException("JWT_AUDIENCE environment variable is missing");

            var expiryString = Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES");
            _expiryMinutes = int.TryParse(expiryString, out var minutes) ? minutes : 60;
        }
        public string GenerateToken(string userId, string username, int roleId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim("username", username),
                new Claim("roleId", roleId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateTempSessionToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);  

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("sub", userId),
                    new Claim("temp_session", "true") 
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public JwtValidationResult? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey); 

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return new JwtValidationResult
                {
                    Principal = principal,
                    ValidTo = validatedToken.ValidTo
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
