using UniversityManagementSystem.Core.Entities.Common;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class College : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? DeanName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}

