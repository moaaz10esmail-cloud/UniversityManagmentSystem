using MediatR;
using UniversityManagementSystem.Application.DTOs.Registration;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Registration.Commands.RegisterForCourses;

public class RegisterForCoursesCommand : IRequest<Result<RegistrationResultDto>>
{
    public Guid StudentId { get; set; }
    public List<Guid> CourseOfferingIds { get; set; } = new();
}
