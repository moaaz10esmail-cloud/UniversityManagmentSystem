using MediatR;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Registration.Commands.DropCourse;

public class DropCourseCommand : IRequest<Result>
{
    public Guid StudentId { get; set; }
    public Guid CourseOfferingId { get; set; }
}
