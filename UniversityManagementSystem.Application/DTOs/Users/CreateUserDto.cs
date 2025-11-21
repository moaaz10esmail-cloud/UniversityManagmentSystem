using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.DTOs.Users;

public class CreateUserDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? NationalId { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public UserType UserType { get; set; }
    public List<string> Roles { get; set; } = new();
}
