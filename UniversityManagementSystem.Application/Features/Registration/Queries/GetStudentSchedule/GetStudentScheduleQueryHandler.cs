using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Registration;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Registration.Queries.GetStudentSchedule;

public class GetStudentScheduleQueryHandler : IRequestHandler<GetStudentScheduleQuery, Result<StudentScheduleDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetStudentScheduleQueryHandler> _logger;

    public GetStudentScheduleQueryHandler(
        IApplicationDbContext context,
        ILogger<GetStudentScheduleQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<StudentScheduleDto>> Handle(GetStudentScheduleQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == request.StudentId, cancellationToken);

        if (student == null)
        {
            return Result<StudentScheduleDto>.Failure("Student not found");
        }

        // Get semester
        Guid semesterId;
        string semesterName;

        if (request.SemesterId.HasValue)
        {
            var semester = await _context.Semesters
                .FirstOrDefaultAsync(s => s.Id == request.SemesterId.Value, cancellationToken);

            if (semester == null)
            {
                return Result<StudentScheduleDto>.Failure("Semester not found");
            }

            semesterId = semester.Id;
            semesterName = $"{semester.Name} {semester.AcademicYear}";
        }
        else
        {
            var currentSemester = await _context.Semesters
                .FirstOrDefaultAsync(s => s.IsActive, cancellationToken);

            if (currentSemester == null)
            {
                return Result<StudentScheduleDto>.Failure("No active semester found");
            }

            semesterId = currentSemester.Id;
            semesterName = $"{currentSemester.Name} {currentSemester.AcademicYear}";
        }

        // Get registered courses
        var registrations = await _context.StudentRegistrations
            .Include(sr => sr.CourseOffering)
                .ThenInclude(co => co.Course)
            .Include(sr => sr.CourseOffering)
                .ThenInclude(co => co.Instructor)
            .Include(sr => sr.CourseOffering)
                .ThenInclude(co => co.ClassSchedules)
            .Where(sr => sr.StudentId == request.StudentId &&
                        sr.CourseOffering.SemesterId == semesterId &&
                        sr.Status == RegistrationStatus.Registered)
            .ToListAsync(cancellationToken);

        var scheduleDto = new StudentScheduleDto
        {
            StudentId = student.Id,
            StudentName = $"{student.User.FirstName} {student.User.LastName}",
            SemesterName = semesterName,
            TotalCredits = registrations.Sum(r => r.CourseOffering.Course.CreditHours)
        };

        foreach (var registration in registrations)
        {
            var courseDto = new ScheduleCourseDto
            {
                CourseOfferingId = registration.CourseOfferingId,
                CourseCode = registration.CourseOffering.Course.Code,
                CourseName = registration.CourseOffering.Course.Name,
                CreditHours = registration.CourseOffering.Course.CreditHours,
                InstructorName = registration.CourseOffering.Instructor != null
                    ? $"{registration.CourseOffering.Instructor.FirstName} {registration.CourseOffering.Instructor.LastName}"
                    : "TBA"
            };

            foreach (var schedule in registration.CourseOffering.ClassSchedules)
            {
                courseDto.ClassSchedules.Add(new ClassScheduleDto
                {
                    DayOfWeek = schedule.DayOfWeek.ToString(),
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime,
                    RoomNumber = schedule.RoomNumber,
                    BuildingName = schedule.BuildingName
                });
            }

            scheduleDto.Courses.Add(courseDto);
        }

        return Result<StudentScheduleDto>.Success(scheduleDto);
    }
}
