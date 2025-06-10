using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<MembershipStatus> MembershipStatuses { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<TeamMembers> TeamMembers { get; set; }
        public DbSet<Priorities> Priorities { get; set; }
        public DbSet<TaskStatuses> TaskStatuses { get; set; }
        public DbSet<Todos> Todos { get; set; }
        public DbSet<TimelineEvent> TimelineEvents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnName("id");
                entity.Property(u => u.Username).HasColumnName("username").IsRequired();
                entity.Property(u => u.Email).HasColumnName("email").IsRequired();
                entity.Property(u => u.PasswordHash).HasColumnName("password_hash").IsRequired();
                entity.Property(u => u.TotpSecret).HasColumnName("totp_secret").IsRequired();
                entity.Property(u => u.RoleId).HasColumnName("role_id").IsRequired();
                entity.Property(u => u.FailedAttempts).HasColumnName("failed_attempts").HasDefaultValue(0);
                entity.Property(u => u.IsLocked).HasColumnName("is_locked").HasDefaultValue(false);
                entity.Property(u => u.Is2faEnabled).HasColumnName("is_2fa_enabled").HasDefaultValue(false);
                entity.Property(u => u.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).HasColumnName("id");
                entity.Property(r => r.Name).HasColumnName("name").IsRequired();
            });

            modelBuilder.Entity<MembershipStatus>().ToTable("membership_status");
            modelBuilder.Entity<Teams>().ToTable("teams");
            modelBuilder.Entity<TeamMembers>(entity =>
            {
                entity.ToTable("team_members");
                entity.HasKey(tm => new { tm.TeamId, tm.UserId });
                entity.Property(tm => tm.TeamId).HasColumnName("team_id");
                entity.Property(tm => tm.UserId).HasColumnName("user_id");
                entity.Property(tm => tm.CreatedAt).HasColumnName("created_at");
                entity.Property(tm => tm.MembershipStatusId).HasColumnName("membership_status_id");
                entity.HasOne<Teams>()
                    .WithMany()
                    .HasForeignKey(tm => tm.TeamId);

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(tm => tm.UserId);
            });
            modelBuilder.Entity<Priorities>().ToTable("priorities");
            modelBuilder.Entity<TaskStatuses>().ToTable("todo_status");
            modelBuilder.Entity<Todos>().ToTable("todos");
            modelBuilder.Entity<TimelineEvent>().ToTable("timeline_events");
        }
    }
}
