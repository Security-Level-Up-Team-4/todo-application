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