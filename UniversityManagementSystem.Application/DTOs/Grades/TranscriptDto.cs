namespace UniversityManagementSystem.Application.DTOs.Grades;

public class TranscriptDto
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentIdNumber { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string College { get; set; } = string.Empty;
    public DateTime? EnrollmentDate { get; set; }
    public DateTime GeneratedDate { get; set; }
    public string GeneratedBy { get; set; } = string.Empty;
    public decimal CumulativeGPA { get; set; }
    public int TotalCreditsAttempted { get; set; }
    public int TotalCreditsEarned { get; set; }
    public List<TranscriptSemesterDto> Semesters { get; set; } = new();
}

public class TranscriptSemesterDto
{
    public string SemesterName { get; set; } = string.Empty;
    public string SemesterCode { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal SemesterGPA { get; set; }
    public List<TranscriptCourseDto> Courses { get; set; } = new();
}

public class TranscriptCourseDto
{
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public string LetterGrade { get; set; } = string.Empty;
    public decimal GradePoints { get; set; }
    public decimal TotalScore { get; set; }
}

public class GradeDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public Guid CourseOfferingId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public decimal TotalScore { get; set; }
    public string LetterGrade { get; set; } = string.Empty;
    public decimal GradePoints { get; set; }
    public string Status { get; set; } = string.Empty;
}
