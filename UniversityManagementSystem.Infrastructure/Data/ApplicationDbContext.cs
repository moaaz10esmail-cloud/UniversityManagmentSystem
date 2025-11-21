using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Entities.Academic;
using UniversityManagementSystem.Core.Entities.Finance;
using UniversityManagementSystem.Core.Entities.HR;
using UniversityManagementSystem.Core.Entities.Identity;
using UniversityManagementSystem.Core.Entities.Library;
using UniversityManagementSystem.Core.Entities.Student;

namespace UniversityManagementSystem.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
    IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Academic DbSets
    public DbSet<College> Colleges => Set<College>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CoursePrerequisite> CoursePrerequisites => Set<CoursePrerequisite>();
    public DbSet<CourseOffering> CourseOfferings => Set<CourseOffering>();
    public DbSet<Lecturer> Lecturers => Set<Lecturer>();
    public DbSet<Semester> Semesters => Set<Semester>();
    public DbSet<ClassSchedule> ClassSchedules => Set<ClassSchedule>();
    public DbSet<Grade> Grades => Set<Grade>();
    
    // Student & Registration DbSets
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentRegistration> StudentRegistrations => Set<StudentRegistration>();
    public DbSet<Waitlist> Waitlists => Set<Waitlist>();
    public DbSet<RegistrationHistory> RegistrationHistories => Set<RegistrationHistory>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    
    // Finance DbSets
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    
    // Library DbSets
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookLoan> BookLoans => Set<BookLoan>();
    
    // HR DbSets
    public DbSet<Staff> Staffs => Set<Staff>();
    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
    
    // Identity DbSets
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply all configurations from assembly
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Global query filter for soft deletable entities
        builder.Entity<Department>().HasQueryFilter(e => !e.IsDeleted);
        builder.Entity<Course>().HasQueryFilter(e => !e.IsDeleted);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
