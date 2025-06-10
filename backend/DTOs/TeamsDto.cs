namespace backend.DTOs;

public class TeamsDto
{
    public string Name { get; set; }
    public Guid CreatedBy { get; set; }
}

public class CreateTeamDto
{
    public string Name { get; set; }
}
public class TeamDetailsDto
{
    public Guid TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string Creator { get; set; } = string.Empty;
    public List<TodosDTO>? Todos { get; set; } = new();
    public List<UserDto>? Users { get; set; } = new();
}