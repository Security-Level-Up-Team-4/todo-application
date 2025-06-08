using System.ComponentModel.DataAnnotations;
namespace backend.Models;

public class TeamMembers
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    public Guid TeamId { get; set; }

    [Required]
    public int MembershipStatusId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
