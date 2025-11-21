using UniversityManagementSystem.Core.Entities.Common;

namespace UniversityManagementSystem.Core.Entities.Identity;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public DateTime? Revoked { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
    public Guid UserId { get; set; }

    // Navigation property
    public virtual ApplicationUser User { get; set; } = null!;
}

