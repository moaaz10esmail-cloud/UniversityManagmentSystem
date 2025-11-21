namespace UniversityManagementSystem.Application.DTOs.Dashboard;

public class DashboardStatsDto
{
    public int TotalStudents { get; set; }
    public int ActiveStudents { get; set; }
    public int TotalStaff { get; set; }
    public int TotalCourses { get; set; }
    public int ActiveCourseOfferings { get; set; }
    public int TotalDepartments { get; set; }
    public int TotalColleges { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal OutstandingBalance { get; set; }
    public int BooksInLibrary { get; set; }
    public int BooksOnLoan { get; set; }
    public List<EnrollmentTrendDto> EnrollmentTrends { get; set; } = new();
    public List<TopCourseDto> TopCourses { get; set; } = new();
}

public class EnrollmentTrendDto
{
    public string SemesterName { get; set; } = string.Empty;
    public int StudentCount { get; set; }
    public DateTime StartDate { get; set; }
}

public class TopCourseDto
{
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int EnrollmentCount { get; set; }
}

public class EnrollmentStatsDto
{
    public string SemesterName { get; set; } = string.Empty;
    public int TotalEnrollments { get; set; }
    public int UniqueStudents { get; set; }
    public decimal AverageCreditsPerStudent { get; set; }
    public List<DepartmentEnrollmentDto> DepartmentBreakdown { get; set; } = new();
}

public class DepartmentEnrollmentDto
{
    public string DepartmentName { get; set; } = string.Empty;
    public int StudentCount { get; set; }
    public int CourseCount { get; set; }
}

public class RevenueReportDto
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal TotalInvoiced { get; set; }
    public decimal TotalCollected { get; set; }
    public decimal OutstandingBalance { get; set; }
    public List<MonthlyRevenueDto> MonthlyBreakdown { get; set; } = new();
}

public class MonthlyRevenueDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public decimal Invoiced { get; set; }
    public decimal Collected { get; set; }
}
