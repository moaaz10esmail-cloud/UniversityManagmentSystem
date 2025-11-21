using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.DTOs.Academic;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CreditHours { get; set; }
    public int PracticalHours { get; set; }
    public int TheoreticalHours { get; set; }
    public CourseLevel Level { get; set; }
    public CourseType Type { get; set; }
    public bool IsActive { get; set; }
    public bool HasPrerequisites { get; set; }
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public List<Guid> PrerequisiteCourseIds { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
