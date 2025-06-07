using System;
using System.Security.Claims;

namespace backend.Models  
{
    public class JwtValidationResult
    {
        public ClaimsPrincipal Principal { get; set; } = null!;
        public DateTime ValidTo { get; set; }
    }
}
