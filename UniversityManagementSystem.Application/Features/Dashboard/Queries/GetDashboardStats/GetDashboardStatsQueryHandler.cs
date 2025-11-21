using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Dashboard;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Dashboard.Queries.GetDashboardStats;

public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, Result<DashboardStatsDto>>
{
    private readonly IApplicationDbContext _context;

    public GetDashboardStatsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<DashboardStatsDto>> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var stats = new DashboardStatsDto
        {
            TotalStudents = await _context.Students.CountAsync(cancellationToken),
            ActiveStudents = await _context.Students.CountAsync(s => s.Status == StudentStatus.Active, cancellationToken),
            TotalStaff = await _context.Staffs.CountAsync(cancellationToken),
            TotalCourses = await _context.Courses.CountAsync(cancellationToken),
            ActiveCourseOfferings = await _context.CourseOfferings.CountAsync(co => co.Semester.IsActive, cancellationToken),
            TotalDepartments = await _context.Departments.CountAsync(cancellationToken),
            TotalColleges = await _context.Colleges.CountAsync(cancellationToken),
            TotalRevenue = await _context.Invoices.SumAsync(i => i.TotalAmount, cancellationToken),
            OutstandingBalance = await _context.Invoices.SumAsync(i => i.Balance, cancellationToken),
            BooksInLibrary = await _context.Books.SumAsync(b => b.TotalCopies, cancellationToken),
            BooksOnLoan = await _context.BookLoans.CountAsync(bl => bl.Status == LoanStatus.Active, cancellationToken)
        };

        // Get enrollment trends
        var semesters = await _context.Semesters
            .OrderByDescending(s => s.StartDate)
            .Take(6)
            .ToListAsync(cancellationToken);

        foreach (var semester in semesters)
        {
            var enrollmentCount = await _context.StudentRegistrations
                .CountAsync(sr => sr.CourseOffering.SemesterId == semester.Id && sr.Status == RegistrationStatus.Registered, 
                           cancellationToken);

            stats.EnrollmentTrends.Add(new EnrollmentTrendDto
            {
                SemesterName = $"{semester.Name} {semester.AcademicYear}",
                StudentCount = enrollmentCount,
                StartDate = semester.StartDate
            });
        }

        // Get top courses
        var topCourses = await _context.StudentRegistrations
            .Where(sr => sr.Status == RegistrationStatus.Registered)
            .GroupBy(sr => new { sr.CourseOffering.Course.Code, sr.CourseOffering.Course.Name })
            .Select(g => new TopCourseDto
            {
                CourseCode = g.Key.Code,
                CourseName = g.Key.Name,
                EnrollmentCount = g.Count()
            })
            .OrderByDescending(tc => tc.EnrollmentCount)
            .Take(5)
            .ToListAsync(cancellationToken);

        stats.TopCourses = topCourses;

        return Result<DashboardStatsDto>.Success(stats);
    }
}
