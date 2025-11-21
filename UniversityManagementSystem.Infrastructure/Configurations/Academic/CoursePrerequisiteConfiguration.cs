using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityManagementSystem.Core.Entities.Academic;

namespace UniversityManagementSystem.Infrastructure.Configurations.Academic;

public class CoursePrerequisiteConfiguration : IEntityTypeConfiguration<CoursePrerequisite>
{
    public void Configure(EntityTypeBuilder<CoursePrerequisite> builder)
    {
        builder.ToTable("CoursePrerequisites", table =>
        {
            // Prevent a course from being a prerequisite of itself
            table.HasCheckConstraint(
                "CK_CoursePrerequisite_NotSelfReference",
                "[CourseId] <> [PrerequisiteCourseId]");
        });

        // Relationships
        builder.HasOne(cp => cp.Course)
            .WithMany(c => c.Prerequisites)
            .HasForeignKey(cp => cp.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cp => cp.PrerequisiteCourse)
            .WithMany(c => c.IsPrerequisiteFor)
            .HasForeignKey(cp => cp.PrerequisiteCourseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes - Prevent duplicate prerequisites
        builder.HasIndex(cp => new { cp.CourseId, cp.PrerequisiteCourseId })
            .IsUnique();
    }
}

