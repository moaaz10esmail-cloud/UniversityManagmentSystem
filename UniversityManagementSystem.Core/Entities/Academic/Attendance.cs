using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class Attendance : AuditableEntity
{
    public Guid StudentId { get; set; }
    public Guid CourseOfferingId { get; set; }
    public DateTime AttendanceDate { get; set; }
    public AttendanceStatus Status { get; set; }
    public string? Notes { get; set; }
    public TimeSpan? TimeIn { get; set; }
    public TimeSpan? TimeOut { get; set; }
    
    // Navigation properties
    public virtual Student Student { get; set; } = null!;
    public virtual CourseOffering CourseOffering { get; set; } = null!;
}
