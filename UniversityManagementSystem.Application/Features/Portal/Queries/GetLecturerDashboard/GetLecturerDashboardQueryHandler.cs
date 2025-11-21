using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Portal;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Portal.Queries.GetLecturerDashboard;

public class GetLecturerDashboardQueryHandler : IRequestHandler<GetLecturerDashboardQuery, Result<LecturerDashboardDto>>
{
    private readonly IApplicationDbContext _context;

    public GetLecturerDashboardQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<LecturerDashboardDto>> Handle(GetLecturerDashboardQuery request, CancellationToken cancellationToken)
    {
        var lecturer = await _context.Lecturers
            .Include(l => l.Department)
            .FirstOrDefaultAsync(l => l.Id == request.LecturerId, cancellationToken);

        if (lecturer == null)
        {
            return Result<LecturerDashboardDto>.Failure("Lecturer not found");
        }

        var currentCourses = await _context.CourseOfferings
            .Include(co => co.Course)
            .Include(co => co.Semester)
            .Where(co => co.InstructorId == request.LecturerId && co.Semester.IsActive)
            .ToListAsync(cancellationToken);

        var totalStudents = await _context.StudentRegistrations
            .Where(sr => currentCourses.Select(c => c.Id).Contains(sr.CourseOfferingId) && sr.Status == RegistrationStatus.Registered)
            .CountAsync(cancellationToken);

        var pendingGrades = await _context.StudentRegistrations
            .Where(sr => currentCourses.Select(c => c.Id).Contains(sr.CourseOfferingId) && 
                        sr.Status == RegistrationStatus.Registered &&
                        !_context.Grades.Any(g => g.StudentRegistrationId == sr.Id))
            .CountAsync(cancellationToken);

        var dashboard = new LecturerDashboardDto
        {
            LecturerId = lecturer.Id,
            LecturerName = $"{lecturer.FirstName} {lecturer.LastName}",
            DepartmentName = lecturer.Department.Name,
            TotalCourses = currentCourses.Count,
            TotalStudents = totalStudents,
            PendingGrades = pendingGrades
        };

        foreach (var course in currentCourses)
        {
            var enrolledCount = await _context.StudentRegistrations
                .CountAsync(sr => sr.CourseOfferingId == course.Id && sr.Status == RegistrationStatus.Registered, cancellationToken);

            dashboard.CurrentCourses.Add(new LecturerCourseDto
            {
                CourseOfferingId = course.Id,
                CourseCode = course.Course.Code,
                CourseName = course.Course.Name,
                EnrolledStudents = enrolledCount,
                SemesterName = course.Semester.Name,
                AverageAttendance = 0 // TODO: Calculate
            });
        }

        return Result<LecturerDashboardDto>.Success(dashboard);
    }
}
