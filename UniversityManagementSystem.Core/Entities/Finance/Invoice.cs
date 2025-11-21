using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Finance;

public class Invoice : AuditableEntity
{
    public Guid StudentId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal Balance => TotalAmount - PaidAmount;
    public InvoiceStatus Status { get; set; }
    public string? Notes { get; set; }
    
    // Navigation properties
    public virtual ApplicationUser Student { get; set; } = null!;
    public virtual ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
