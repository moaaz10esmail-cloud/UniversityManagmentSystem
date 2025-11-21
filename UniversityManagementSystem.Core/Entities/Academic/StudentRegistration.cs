using UniversityManagementSystem.Core.Entities.Common;
using UniversityManagementSystem.Core.Entities.Identity;
using UniversityManagementSystem.Core.Enums;
using StudentEntity = UniversityManagementSystem.Core.Entities.Student.Student;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class StudentRegistration : AuditableEntity
{
    public Guid StudentId { get; set; }
    public Guid CourseOfferingId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public RegistrationStatus Status { get; set; }
    public RegistrationType Type { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public string? ApprovedBy { get; set; }
    public string? Notes { get; set; }
    
    // Navigation properties
    public virtual StudentEntity Student { get; set; } = null!;
    public virtual CourseOffering CourseOffering { get; set; } = null!;
    public virtual ICollection<RegistrationHistory> History { get; set; } = new List<RegistrationHistory>();
    public virtual Grade? Grade { get; set; }
}
