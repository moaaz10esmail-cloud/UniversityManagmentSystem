using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityManagementSystem.Core.Entities.Academic;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Infrastructure.Configurations.Academic;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.Property(c => c.Code)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.CreditHours)
            .IsRequired();

        builder.Property(c => c.Level)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(c => c.Type)
            .HasConversion<int>()
            .IsRequired();

        // Relationship
        builder.HasOne(c => c.Department)
            .WithMany(d => d.Courses)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(c => c.Code)
            .IsUnique();

        builder.HasIndex(c => new { c.DepartmentId, c.Code })
            .IsUnique();

        builder.HasIndex(c => c.IsActive);
    }
}
