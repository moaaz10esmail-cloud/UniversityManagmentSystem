using Microsoft.AspNetCore.Identity;
using UniversityManagementSystem.Core.Entities.Academic;
using StudentEntity = UniversityManagementSystem.Core.Entities.Student.Student;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? NationalId { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public UserType UserType { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public StudentEntity? Student { get; set; }
    public Lecturer? Lecturer { get; set; }
}

