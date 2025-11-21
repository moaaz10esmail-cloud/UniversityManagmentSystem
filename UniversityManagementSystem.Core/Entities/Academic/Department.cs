using UniversityManagementSystem.Core.Entities.Common;
using StudentEntity = UniversityManagementSystem.Core.Entities.Student.Student;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class Department : SoftDeletableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? HeadOfDepartment { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid CollegeId { get; set; }

    // Navigation properties
    public virtual College College { get; set; } = null!;
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();
    public virtual ICollection<Lecturer> Lecturers { get; set; } = new List<Lecturer>();
}


