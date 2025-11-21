using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.DTOs.Registration;

public class RegistrationResultDto
{
    public List<RegistrationSuccessDto> SuccessfulRegistrations { get; set; } = new();
    public List<RegistrationErrorDto> FailedRegistrations { get; set; } = new();
    public int TotalRegistered { get; set; }
    public int TotalWaitlisted { get; set; }
    public int TotalFailed { get; set; }
}

public class RegistrationSuccessDto
{
    public Guid CourseOfferingId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public RegistrationStatus Status { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class RegistrationErrorDto
{
    public Guid CourseOfferingId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
}
