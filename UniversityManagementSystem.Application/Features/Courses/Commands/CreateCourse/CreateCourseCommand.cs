using MediatR;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommand : IRequest<Result<Guid>>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CreditHours { get; set; }
    public int PracticalHours { get; set; }
    public int TheoreticalHours { get; set; }
    public CourseLevel Level { get; set; }
    public CourseType Type { get; set; }
    public Guid DepartmentId { get; set; }
    public List<Guid> PrerequisiteCourseIds { get; set; } = new();
}
