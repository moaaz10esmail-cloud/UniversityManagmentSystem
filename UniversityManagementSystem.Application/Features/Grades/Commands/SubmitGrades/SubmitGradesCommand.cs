using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Grades;

namespace UniversityManagementSystem.Application.Features.Grades.Commands.SubmitGrades;

public class SubmitGradesCommand : IRequest<Result>
{
    public Guid CourseOfferingId { get; set; }
    public List<GradeSubmission> Grades { get; set; } = new();
}

public class GradeSubmission
{
    public Guid StudentRegistrationId { get; set; }
    public decimal TotalScore { get; set; }
    public string LetterGrade { get; set; } = string.Empty;
    public decimal GradePoints { get; set; }
}
