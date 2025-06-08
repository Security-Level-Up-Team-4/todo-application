namespace backend.DTOs;

public class TeamMemberDto
{
    public Guid TeamId { get; set; }
    public Guid UserId { get; set; }
    public int MembershipStatusId { get; set; }
}

public class AddTeamMemberRequest
{
    public string Username { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
}

