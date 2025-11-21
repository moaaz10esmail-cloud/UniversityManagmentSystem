using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Academic;
using UniversityManagementSystem.Core.Entities.Identity;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.HR;

public class Staff : AuditableEntity
{
    public Guid UserId { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public Guid DepartmentId { get; set; }
    public StaffPosition Position { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public EmploymentStatus Status { get; set; }
    public string? Qualifications { get; set; }
    public string? Specialization { get; set; }
    
    // Navigation properties
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;
    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
