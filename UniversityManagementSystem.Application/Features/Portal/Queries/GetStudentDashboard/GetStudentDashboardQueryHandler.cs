using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Portal;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Portal.Queries.GetStudentDashboard;

public class GetStudentDashboardQueryHandler : IRequestHandler<GetStudentDashboardQuery, Result<StudentDashboardDto>>
{
    private readonly IApplicationDbContext _context;

    public GetStudentDashboardQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<StudentDashboardDto>> Handle(GetStudentDashboardQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .Include(s => s.User)
            .Include(s => s.Department)
            .FirstOrDefaultAsync(s => s.Id == request.StudentId, cancellationToken);

        if (student == null)
        {
            return Result<StudentDashboardDto>.Failure("Student not found");
        }

        var currentSemester = await _context.Semesters
            .FirstOrDefaultAsync(s => s.IsActive, cancellationToken);

        var currentRegistrations = await _context.StudentRegistrations
            .Include(sr => sr.CourseOffering)
                .ThenInclude(co => co.Course)
            .Where(sr => sr.StudentId == request.StudentId && 
                        sr.Status == RegistrationStatus.Registered &&
                        sr.CourseOffering.Semester.IsActive)
            .ToListAsync(cancellationToken);

        var financialBalance = await _context.Invoices
            .Where(i => i.StudentId == student.UserId)
            .SumAsync(i => i.Balance, cancellationToken);

        var booksOnLoan = await _context.BookLoans
            .CountAsync(bl => bl.StudentId == student.UserId && bl.Status == LoanStatus.Active, cancellationToken);

        var dashboard = new StudentDashboardDto
        {
            StudentId = student.Id,
            StudentName = $"{student.User.FirstName} {student.User.LastName}",
            StudentIdNumber = student.StudentId,
            DepartmentName = student.Department.Name,
            AcademicYear = student.AcademicYear,
            GPA = student.GPA,
            CGPA = student.CGPA,
            TotalCredits = student.TotalCredits,
            CurrentSemesterCredits = currentRegistrations.Sum(r => r.CourseOffering.Course.CreditHours),
            CurrentCourseCount = currentRegistrations.Count,
            FinancialBalance = financialBalance,
            BooksOnLoan = booksOnLoan,
            AttendancePercentage = 0 // TODO: Calculate from attendance records
        };

        return Result<StudentDashboardDto>.Success(dashboard);
    }
}
