using UniversityManagementSystem.Core.Entities.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class Semester : AuditableEntity
{
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
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public virtual ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();
}
