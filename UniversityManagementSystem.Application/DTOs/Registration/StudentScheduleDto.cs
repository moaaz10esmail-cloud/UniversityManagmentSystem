namespace UniversityManagementSystem.Application.DTOs.Registration;

public class StudentScheduleDto
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string SemesterName { get; set; } = string.Empty;
    public List<ScheduleCourseDto> Courses { get; set; } = new();
    public int TotalCredits { get; set; }
}

public class ScheduleCourseDto
{
    public Guid CourseOfferingId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public string InstructorName { get; set; } = string.Empty;
    public List<ClassScheduleDto> ClassSchedules { get; set; } = new();
}

public class ClassScheduleDto
{
    public string DayOfWeek { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? RoomNumber { get; set; }
    public string? BuildingName { get; set; }
}
