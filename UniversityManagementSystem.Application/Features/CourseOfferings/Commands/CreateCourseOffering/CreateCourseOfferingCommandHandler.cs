using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Contracts.Persistence;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Academic;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.CourseOfferings.Commands.CreateCourseOffering;

public class CreateCourseOfferingCommandHandler : IRequestHandler<CreateCourseOfferingCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateCourseOfferingCommandHandler> _logger;

    public CreateCourseOfferingCommandHandler(IApplicationDbContext context, ILogger<CreateCourseOfferingCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateCourseOfferingCommand request, CancellationToken cancellationToken)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == request.CourseId, cancellationToken);

        if (course == null)
        {
            return Result<Guid>.Failure("Course not found");
        }

        var semester = await _context.Semesters
            .FirstOrDefaultAsync(s => s.Id == request.SemesterId && s.IsActive, cancellationToken);

        if (semester == null)
        {
            return Result<Guid>.Failure("Semester not found or inactive");
        }

        if (request.LecturerId.HasValue)
        {
            var lecturer = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.LecturerId.Value && u.UserType == UserType.Lecturer, cancellationToken);

            if (lecturer == null)
            {
                return Result<Guid>.Failure("Lecturer not found");
            }
        }

        var existingOffering = await _context.CourseOfferings
            .FirstOrDefaultAsync(co => 
                co.CourseId == request.CourseId && 
                co.SemesterId == request.SemesterId && 
                co.Section == request.Section, cancellationToken);

        if (existingOffering != null)
        {
            return Result<Guid>.Failure("Course offering already exists for this course, semester, and section");
        }

        var courseOffering = new CourseOffering
        {
            CourseId = request.CourseId,
            SemesterId = request.SemesterId,
            LecturerId = request.LecturerId,
            Section = request.Section,
            Capacity = request.Capacity,
            EnrolledCount = 0,
            Status = CourseOfferingStatus.Published
        };

        _context.CourseOfferings.Add(courseOffering);
        await _context.SaveChangesAsync(cancellationToken);

        foreach (var scheduleDto in request.ClassSchedules)
        {
            var classSchedule = new ClassSchedule
            {
                CourseOfferingId = courseOffering.Id,
                DayOfWeek = scheduleDto.DayOfWeek,
                StartTime = scheduleDto.StartTime,
                EndTime = scheduleDto.EndTime,
                Room = scheduleDto.Room,
                ClassType = scheduleDto.ClassType
            };
            _context.ClassSchedules.Add(classSchedule);
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Course offering created for {CourseCode} section {Section} in semester {SemesterCode}",
            course.Code, request.Section, semester.Code);

        return Result<Guid>.Success(courseOffering.Id, "Course offering created successfully");
    }
}
