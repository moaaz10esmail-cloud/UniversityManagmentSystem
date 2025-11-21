using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityManagementSystem.Core.Entities.Academic;

namespace UniversityManagementSystem.Infrastructure.Configurations.Academic;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");

        builder.Property(d => d.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.Code)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(d => d.Description)
            .HasMaxLength(500);

        builder.Property(d => d.HeadOfDepartment)
            .HasMaxLength(100);

        builder.Property(d => d.ContactEmail)
            .HasMaxLength(100);

        // Relationship
        builder.HasOne(d => d.College)
            .WithMany(c => c.Departments)
            .HasForeignKey(d => d.CollegeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(d => d.Code)
            .IsUnique();

        builder.HasIndex(d => new { d.CollegeId, d.Code })
            .IsUnique();

        builder.HasIndex(d => d.IsActive);
    }
}
