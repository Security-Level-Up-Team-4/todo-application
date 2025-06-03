using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Repositories;
using backend.Services;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// PostgreSQL connection string (adjust with your own values)
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRolesService, RolesService>();

builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
builder.Services.AddScoped<ITeamMembersService, TeamMembersService>();

builder.Services.AddScoped<IMembershipStatusRepository, MembershipStatusRepository>();
builder.Services.AddScoped<IMembershipStatusService, MembershipStatusService>();

builder.Services.AddScoped<ITeamsRepository, TeamRepository>();
builder.Services.AddScoped<ITeamsService, TeamsService>();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

builder.Services.AddDbContext<TodoContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
