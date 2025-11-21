using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.DTOs.Academic;

public class CourseOfferingDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public Guid SemesterId { get; set; }
    public string SemesterName { get; set; } = string.Empty;
    public Guid? LecturerId { get; set; }
    public string? LecturerName { get; set; }
    public string Section { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int EnrolledCount { get; set; }
    public int AvailableSeats { get; set; }
    public bool IsFull { get; set; }
    public CourseOfferingStatus Status { get; set; }
    public List<ClassScheduleDto> ClassSchedules { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

public class ClassScheduleDto
{
    public Guid Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Room { get; set; } = string.Empty;
    public ClassType ClassType { get; set; }
}
