using UniversityManagementSystem.Core.Entities.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class ClassSchedule : BaseEntity
{
    public Guid CourseOfferingId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Room { get; set; } = string.Empty;
    public ClassType ClassType { get; set; }
    
    // Navigation properties
    public virtual CourseOffering CourseOffering { get; set; } = null!;
}
