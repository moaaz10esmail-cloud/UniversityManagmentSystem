using UniversityManagementSystem.Core.Entities.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Academic;

public class RegistrationHistory : BaseEntity
{
    public Guid StudentRegistrationId { get; set; }
    public RegistrationStatus FromStatus { get; set; }
    public RegistrationStatus ToStatus { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string? ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
    
    // Navigation properties
    public virtual StudentRegistration StudentRegistration { get; set; } = null!;
}
