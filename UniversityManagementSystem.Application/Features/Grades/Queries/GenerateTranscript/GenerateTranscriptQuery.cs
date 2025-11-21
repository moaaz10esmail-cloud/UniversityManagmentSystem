using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Grades;

namespace UniversityManagementSystem.Application.Features.Grades.Queries.GenerateTranscript;

public class GenerateTranscriptQuery : IRequest<Result<TranscriptDto>>
{
    public Guid StudentId { get; set; }
    public bool IncludeCurrentSemester { get; set; } = true;
}
