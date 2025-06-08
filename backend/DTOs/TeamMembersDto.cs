namespace backend.DTOs;

public class TeamMemberDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
}

public class AddTeamMemberRequest
{
    public string Username { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
}

public class RemoveTeamMembersDto
{
    public Guid TeamId { get; set; }
    public List<Guid> UserIds { get; set; } = new();
}

