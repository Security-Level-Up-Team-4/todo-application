using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("users")]
public class Users
{
    [Key]
    public Guid Id { get; set; }

    [Required, MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required, Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Required, Column("totp_secret")]
    public string TotpSecret { get; set; } = string.Empty;

    [Required, Column("role_id")]
    public int RoleId { get; set; }

    [Required, Column("failed_attempts")]
    public int FailedAttempts { get; set; } = 0;

    [Required, Column("is_locked")]
    public bool IsLocked { get; set; } = false;

    [Required, Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
