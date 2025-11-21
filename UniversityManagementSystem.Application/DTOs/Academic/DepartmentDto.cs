namespace UniversityManagementSystem.Application.DTOs.Academic;

public class DepartmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? HeadOfDepartment { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public bool IsActive { get; set; }
    public Guid CollegeId { get; set; }
    public string CollegeName { get; set; } = string.Empty;
    public int CoursesCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
