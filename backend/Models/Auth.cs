using System;

namespace backend.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string TotpSecret { get; set; } = null!;
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } 
        public int FailedAttempts { get; set; }
        public bool IsLocked { get; set; }
        public bool Is2faEnabled { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Role
    {
        public  int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
