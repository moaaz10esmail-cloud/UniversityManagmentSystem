using UniversityManagementSystem.Core.Entities.Common;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class Lecturer : BaseEntity
{
    public Guid UserId { get; set; }
    public string EmployeeNumber { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public Guid DepartmentId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;
}

