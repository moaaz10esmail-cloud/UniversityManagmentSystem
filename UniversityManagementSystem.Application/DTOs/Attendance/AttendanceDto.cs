namespace UniversityManagementSystem.Application.DTOs.Attendance;

public class AttendanceDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public Guid CourseOfferingId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public DateTime AttendanceDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public TimeSpan? TimeIn { get; set; }
    public TimeSpan? TimeOut { get; set; }
    public string? Notes { get; set; }
}

public class AttendanceReportDto
{
    public Guid CourseOfferingId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int TotalSessions { get; set; }
    public List<StudentAttendanceSummaryDto> StudentSummaries { get; set; } = new();
}

public class StudentAttendanceSummaryDto
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int PresentCount { get; set; }
    public int AbsentCount { get; set; }
    public int LateCount { get; set; }
    public int ExcusedCount { get; set; }
    public decimal AttendancePercentage { get; set; }
}

public class MarkAttendanceDto
{
    public Guid StudentId { get; set; }
    public string Status { get; set; } = string.Empty;
    public TimeSpan? TimeIn { get; set; }
    public string? Notes { get; set; }
}
