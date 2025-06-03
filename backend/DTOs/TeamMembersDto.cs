namespace backend.DTOs;

public class TeamMemberDto
{
    public Guid TeamId { get; set; }
    public Guid UserId { get; set; }
    public int MembershipStatusId { get; set; }
}
