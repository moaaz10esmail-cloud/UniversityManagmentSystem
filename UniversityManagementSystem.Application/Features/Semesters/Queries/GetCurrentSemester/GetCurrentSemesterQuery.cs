using MediatR;
using UniversityManagementSystem.Application.DTOs.Academic;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Semesters.Queries.GetCurrentSemester;

public class GetCurrentSemesterQuery : IRequest<Result<SemesterDto>>
{
}
