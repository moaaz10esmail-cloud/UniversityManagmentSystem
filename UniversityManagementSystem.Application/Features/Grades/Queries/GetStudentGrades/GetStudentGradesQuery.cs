using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Grades;

namespace UniversityManagementSystem.Application.Features.Grades.Queries.GetStudentGrades;

public class GetStudentGradesQuery : IRequest<Result<List<GradeDto>>>
{
    public Guid StudentId { get; set; }
    public Guid? SemesterId { get; set; }
}
