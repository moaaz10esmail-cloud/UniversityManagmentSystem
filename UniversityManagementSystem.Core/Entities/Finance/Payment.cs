using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Finance;

public class Payment : AuditableEntity
{
    public Guid InvoiceId { get; set; }
    public string PaymentNumber { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public string? ReferenceNumber { get; set; }
    public PaymentStatus Status { get; set; }
    public string? Notes { get; set; }
    
    // Navigation properties
    public virtual Invoice Invoice { get; set; } = null!;
}
