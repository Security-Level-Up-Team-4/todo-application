using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
DotNetEnv.Env.Load(); // Load from .env file

var builder = WebApplication.CreateBuilder(args);

// Read connection string
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");


// Register DbContext
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
