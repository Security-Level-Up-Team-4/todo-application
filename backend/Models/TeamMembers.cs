using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models;

public class TeamMembers
{
    [Key, Column("user_id")]
    public Guid UserId { get; set; }

    [Required, Column("team_id")]
    public Guid TeamId { get; set; }

    [Required, Column("membership_status_id")]
    public int MembershipStatusId { get; set; }

    [Required, Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
