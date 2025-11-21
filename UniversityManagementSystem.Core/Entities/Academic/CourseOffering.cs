using UniversityManagementSystem.Core.Entities.Common;
using UniversityManagementSystem.Core.Entities.Identity;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class CourseOffering : AuditableEntity
{
    public Guid CourseId { get; set; }
    public Guid SemesterId { get; set; }
    public Guid? LecturerId { get; set; }
    public string Section { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int EnrolledCount { get; set; }
    public int AvailableSeats => Capacity - EnrolledCount;
    public bool IsFull => EnrolledCount >= Capacity;
    public CourseOfferingStatus Status { get; set; }
    
    // Navigation properties
    public virtual Course Course { get; set; } = null!;
    public virtual Semester Semester { get; set; } = null!;
    public virtual ApplicationUser? Lecturer { get; set; }
    public virtual ICollection<ClassSchedule> ClassSchedules { get; set; } = new List<ClassSchedule>();
}
