namespace UniversityManagementSystem.Application.DTOs.Roles;

public class UpdateRoleDto
{
    public string Description { get; set; } = string.Empty;
    public List<string> Permissions { get; set; } = new();
}
