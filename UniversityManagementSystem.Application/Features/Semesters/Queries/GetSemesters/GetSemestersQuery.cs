using MediatR;
using UniversityManagementSystem.Application.DTOs.Academic;
using UniversityManagementSystem.Application.DTOs.Common;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Semesters.Queries.GetSemesters;

public class GetSemestersQuery : PaginationRequest, IRequest<Result<PagedList<SemesterDto>>>
{
    public SemesterType? Type { get; set; }
    public int? AcademicYear { get; set; }
    public SemesterStatus? Status { get; set; }
    public bool? IsActive { get; set; }
}
