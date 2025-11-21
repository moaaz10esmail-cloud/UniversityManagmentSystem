using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Library;

public class BookLoan : AuditableEntity
{
    public Guid BookId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal? FineAmount { get; set; }
    public LoanStatus Status { get; set; }
    public string? Notes { get; set; }
    
    // Navigation properties
    public virtual Book Book { get; set; } = null!;
    public virtual ApplicationUser Student { get; set; } = null!;
}
