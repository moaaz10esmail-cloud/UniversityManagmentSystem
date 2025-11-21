namespace UniversityManagementSystem.Application.DTOs.Roles;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public int UsersCount { get; set; }
    public List<string> Permissions { get; set; } = new();
}
