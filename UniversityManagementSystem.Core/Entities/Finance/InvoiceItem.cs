using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Core.Entities.Finance;

public class InvoiceItem : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal TotalAmount => Amount * Quantity;
    
    // Navigation properties
    public virtual Invoice Invoice { get; set; } = null!;
}
