using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Core.Entities.Library;

public class Book : AuditableEntity
{
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string? Publisher { get; set; }
    public int PublicationYear { get; set; }
    public string? Edition { get; set; }
    public BookCategory Category { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Description { get; set; }
    public string? Location { get; set; } // Shelf location
    
    // Navigation properties
    public virtual ICollection<BookLoan> Loans { get; set; } = new List<BookLoan>();
}
