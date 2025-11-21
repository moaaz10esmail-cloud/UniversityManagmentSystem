namespace UniversityManagementSystem.Application.DTOs.Portal;

public class StudentDashboardDto
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentIdNumber { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public int AcademicYear { get; set; }
    public decimal? GPA { get; set; }
    public decimal? CGPA { get; set; }
    public int TotalCredits { get; set; }
    public int CurrentSemesterCredits { get; set; }
    public int CurrentCourseCount { get; set; }
    public decimal FinancialBalance { get; set; }
    public int BooksOnLoan { get; set; }
    public decimal AttendancePercentage { get; set; }
    public List<UpcomingClassDto> UpcomingClasses { get; set; } = new();
    public List<string> RecentAnnouncements { get; set; } = new();
}

public class UpcomingClassDto
{
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public DateTime ClassDateTime { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string InstructorName { get; set; } = string.Empty;
}

public class LecturerDashboardDto
{
    public Guid LecturerId { get; set; }
    public string LecturerName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public int TotalCourses { get; set; }
    public int TotalStudents { get; set; }
    public int PendingGrades { get; set; }
    public List<LecturerCourseDto> CurrentCourses { get; set; } = new();
    public List<UpcomingClassDto> UpcomingClasses { get; set; } = new();
}

public class LecturerCourseDto
{
    public Guid CourseOfferingId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int EnrolledStudents { get; set; }
    public string SemesterName { get; set; } = string.Empty;
    public decimal AverageAttendance { get; set; }
}
