namespace backend.DTOs;

public class UsersDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public bool IsLocked { get; set; }
    public DateTime CreatedAt { get; set; }
}
