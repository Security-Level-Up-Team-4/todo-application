namespace backend.DTOs;

    public class CreateRolesDto
    {
        public string Name { get; set; } = null!;
    }

    public class RolesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

