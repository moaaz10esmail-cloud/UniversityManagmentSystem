using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Registration;

namespace UniversityManagementSystem.Application.Features.Registration.Queries.GetStudentSchedule;

public class GetStudentScheduleQuery : IRequest<Result<StudentScheduleDto>>
{
    public Guid StudentId { get; set; }
    public Guid? SemesterId { get; set; } // If null, get current semester
}
