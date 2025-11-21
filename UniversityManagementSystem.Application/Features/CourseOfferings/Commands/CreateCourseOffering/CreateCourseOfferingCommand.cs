using MediatR;
using UniversityManagementSystem.Application.DTOs.Academic;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.CourseOfferings.Commands.CreateCourseOffering;

public class CreateCourseOfferingCommand : IRequest<Result<Guid>>
{
    public Guid CourseId { get; set; }
    public Guid SemesterId { get; set; }
    public Guid? LecturerId { get; set; }
    public string Section { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public List<ClassScheduleDto> ClassSchedules { get; set; } = new();
}
