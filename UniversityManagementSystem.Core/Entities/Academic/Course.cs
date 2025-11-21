using UniversityManagementSystem.Core.Entities.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class Course : SoftDeletableEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CreditHours { get; set; }
    public int PracticalHours { get; set; }
    public int TheoreticalHours { get; set; }
    public CourseLevel Level { get; set; }
    public CourseType Type { get; set; }
    public bool IsActive { get; set; } = true;
    public bool HasPrerequisites { get; set; }
    public Guid DepartmentId { get; set; }

    // Navigation properties
    public virtual Department Department { get; set; } = null!;
    public virtual ICollection<CoursePrerequisite> Prerequisites { get; set; } = new List<CoursePrerequisite>();
    public virtual ICollection<CoursePrerequisite> IsPrerequisiteFor { get; set; } = new List<CoursePrerequisite>();
}


