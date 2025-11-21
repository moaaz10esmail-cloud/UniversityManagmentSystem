using MediatR;
using UniversityManagementSystem.Application.DTOs.Academic;
using UniversityManagementSystem.Application.DTOs.Common;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Colleges.Queries.GetColleges;

public class GetCollegesQuery : PaginationRequest, IRequest<Result<PagedList<CollegeDto>>>
{
    public string? SearchTerm { get; set; }
    public bool? IsActive { get; set; }
}
