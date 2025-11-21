using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Core.Entities.Academic;
using UniversityManagementSystem.Core.Entities.Finance;
using UniversityManagementSystem.Core.Entities.HR;
using UniversityManagementSystem.Core.Entities.Identity;
using UniversityManagementSystem.Core.Entities.Library;

namespace UniversityManagementSystem.Application.Interfaces;

public interface IApplicationDbContext
{
    // Academic
    DbSet<College> Colleges { get; }
    DbSet<Department> Departments { get; }
    DbSet<Course> Courses { get; }
    DbSet<CoursePrerequisite> CoursePrerequisites { get; }
    DbSet<CourseOffering> CourseOfferings { get; }
    DbSet<Lecturer> Lecturers { get; }
    DbSet<Semester> Semesters { get; }
    DbSet<ClassSchedule> ClassSchedules { get; }
    DbSet<Grade> Grades { get; }
    
    // Student & Registration
    DbSet<Student> Students { get; }
    DbSet<StudentRegistration> StudentRegistrations { get; }
    DbSet<Waitlist> Waitlists { get; }
    DbSet<RegistrationHistory> RegistrationHistories { get; }
    DbSet<Attendance> Attendances { get; }
    
    // Finance
    DbSet<Invoice> Invoices { get; }
    DbSet<InvoiceItem> InvoiceItems { get; }
    DbSet<Payment> Payments { get; }
    
    // Library
    DbSet<Book> Books { get; }
    DbSet<BookLoan> BookLoans { get; }
    
    // HR
    DbSet<Staff> Staffs { get; }
    DbSet<LeaveRequest> LeaveRequests { get; }
    
    // Identity
    DbSet<RefreshToken> RefreshTokens { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
