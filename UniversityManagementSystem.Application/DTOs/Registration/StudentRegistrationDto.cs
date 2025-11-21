using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.DTOs.Registration;

public class StudentRegistrationDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public Guid CourseOfferingId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? InstructorName { get; set; }
}

public class StudentRegistrationsListDto
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string SemesterName { get; set; } = string.Empty;
    public List<StudentRegistrationDto> Registrations { get; set; } = new();
    public int TotalCredits { get; set; }
}
