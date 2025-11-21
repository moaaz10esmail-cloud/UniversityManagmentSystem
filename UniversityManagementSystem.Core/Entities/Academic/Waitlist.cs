using UniversityManagementSystem.Core.Entities.Common;
using UniversityManagementSystem.Core.Entities.Identity;
using StudentEntity = UniversityManagementSystem.Core.Entities.Student.Student;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class Waitlist : BaseEntity
{
    public Guid StudentId { get; set; }
    public Guid CourseOfferingId { get; set; }
    public int Position { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime? NotifiedDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    
    // Navigation properties
    public virtual StudentEntity Student { get; set; } = null!;
    public virtual CourseOffering CourseOffering { get; set; } = null!;
}
