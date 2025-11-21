using UniversityManagementSystem.Core.Entities.Common;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class CoursePrerequisite : BaseEntity
{
    public Guid CourseId { get; set; }
    public Guid PrerequisiteCourseId { get; set; }

    // Navigation properties
    public virtual Course Course { get; set; } = null!;
    public virtual Course PrerequisiteCourse { get; set; } = null!;
}

