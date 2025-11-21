using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.DTOs.Users;

public class UpdateUserDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? NationalId { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public bool IsActive { get; set; }
}
