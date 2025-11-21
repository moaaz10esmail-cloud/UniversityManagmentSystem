using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Infrastructure.Configurations.Identity;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");

        builder.Property(u => u.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.NationalId)
            .HasMaxLength(20);

        builder.Property(u => u.UserType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(u => u.Gender)
            .HasConversion<int>();

        // Indexes
        builder.HasIndex(u => u.NationalId)
            .IsUnique()
            .HasFilter("[NationalId] IS NOT NULL");

        builder.HasIndex(u => u.UserType);
        builder.HasIndex(u => u.IsActive);
    }
}
