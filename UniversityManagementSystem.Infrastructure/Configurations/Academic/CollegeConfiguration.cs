using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityManagementSystem.Core.Entities.Academic;

namespace UniversityManagementSystem.Infrastructure.Configurations.Academic;

public class CollegeConfiguration : IEntityTypeConfiguration<College>
{
    public void Configure(EntityTypeBuilder<College> builder)
    {
        builder.ToTable("Colleges");

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Code)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.DeanName)
            .HasMaxLength(100);

        builder.Property(c => c.ContactEmail)
            .HasMaxLength(100);

        builder.Property(c => c.ContactPhone)
            .HasMaxLength(20);

        // Indexes
        builder.HasIndex(c => c.Code)
            .IsUnique();

        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder.HasIndex(c => c.IsActive);
    }
}
