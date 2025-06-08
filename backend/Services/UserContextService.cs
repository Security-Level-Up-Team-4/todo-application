using System.Security.Claims;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

public Guid GetUserId()
{
    var user = _httpContextAccessor.HttpContext?.User;
    var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier)
                      ?? user?.FindFirst("sub");

    if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        throw new UnauthorizedAccessException("User ID claim is missing or invalid.");

    return userId;
}
}
