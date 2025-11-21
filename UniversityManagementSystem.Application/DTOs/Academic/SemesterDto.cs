using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.DTOs.Academic;

public class SemesterDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public SemesterType Type { get; set; }
    public int AcademicYear { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime RegistrationStartDate { get; set; }
    public DateTime RegistrationEndDate { get; set; }
    public DateTime AddDropDeadline { get; set; }
    public DateTime WithdrawalDeadline { get; set; }
    public SemesterStatus Status { get; set; }
    public bool IsActive { get; set; }
    public int CourseOfferingsCount { get; set; }
    public bool IsRegistrationOpen => Status == SemesterStatus.RegistrationOpen;
    public bool IsInProgress => Status == SemesterStatus.InProgress;
    public DateTime CreatedAt { get; set; }
}
