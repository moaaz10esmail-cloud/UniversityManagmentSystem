using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Contracts.Persistence;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Academic;

namespace UniversityManagementSystem.Application.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateCourseCommandHandler> _logger;

    public CreateCourseCommandHandler(IApplicationDbContext context, ILogger<CreateCourseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var department = await _context.Departments
            .FirstOrDefaultAsync(d => d.Id == request.DepartmentId, cancellationToken);

        if (department == null)
        {
            return Result<Guid>.Failure("Department not found");
        }

        var existingCourse = await _context.Courses
            .FirstOrDefaultAsync(c => c.Code == request.Code && c.DepartmentId == request.DepartmentId, cancellationToken);

        if (existingCourse != null)
        {
            return Result<Guid>.Failure("Course code already exists in this department");
        }

        var course = new Course
        {
            Code = request.Code,
            Name = request.Name,
            Description = request.Description,
            CreditHours = request.CreditHours,
            PracticalHours = request.PracticalHours,
            TheoreticalHours = request.TheoreticalHours,
            Level = request.Level,
            Type = request.Type,
            DepartmentId = request.DepartmentId,
            HasPrerequisites = request.PrerequisiteCourseIds.Any(),
            IsActive = true
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);

        if (request.PrerequisiteCourseIds.Any())
        {
            foreach (var prerequisiteCourseId in request.PrerequisiteCourseIds)
            {
                var prerequisite = new CoursePrerequisite
                {
                    CourseId = course.Id,
                    PrerequisiteCourseId = prerequisiteCourseId
                };
                _context.CoursePrerequisites.Add(prerequisite);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        _logger.LogInformation("Course {CourseCode} created in department {DepartmentName}", 
            course.Code, department.Name);

        return Result<Guid>.Success(course.Id, "Course created successfully");
    }
}
