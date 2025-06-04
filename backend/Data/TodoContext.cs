using Microsoft.EntityFrameworkCore;
using backend.Models;
namespace backend.Data;
public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {
    }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<MembershipStatus> MembershipStatuses { get; set; }
    public DbSet<Teams> Teams { get; set; }
    public DbSet<TeamMembers> TeamMembers { get; set; }
    public DbSet<Priorities> Priorities { get; set; }
    public DbSet<TaskStatuses> TaskStatuses { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Roles>().ToTable("roles");
        modelBuilder.Entity<MembershipStatus>().ToTable("membership_status");
        modelBuilder.Entity<Teams>().ToTable("teams");
        modelBuilder.Entity<TeamMembers>().ToTable("team_members");
        modelBuilder.Entity<Priorities>().ToTable("priorities");
        modelBuilder.Entity<TaskStatuses>().ToTable("task_status");
    }
}
