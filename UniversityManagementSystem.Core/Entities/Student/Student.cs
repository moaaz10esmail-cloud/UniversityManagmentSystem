using UniversityManagementSystem.Core.Entities.Academic;
using UniversityManagementSystem.Core.Entities.Common;
using UniversityManagementSystem.Core.Entities.Identity;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Student;

public class Student : AuditableEntity
{
    public Guid UserId { get; set; }
    public string StudentId { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public int AcademicYear { get; set; }
    public StudentStatus Status { get; set; }
    public DateTime? EnrollmentDate { get; set; }
    public decimal? GPA { get; set; }
    public decimal? CGPA { get; set; }
    public int TotalCredits { get; set; }
    
    // Navigation properties
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;
    public virtual ICollection<StudentRegistration> Registrations { get; set; } = new List<StudentRegistration>();
}
