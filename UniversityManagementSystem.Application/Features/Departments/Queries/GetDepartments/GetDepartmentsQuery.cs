using MediatR;
using UniversityManagementSystem.Application.DTOs.Academic;
using UniversityManagementSystem.Application.DTOs.Common;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Departments.Queries.GetDepartments;

public class GetDepartmentsQuery : PaginationRequest, IRequest<Result<PagedList<DepartmentDto>>>
{
    public string? SearchTerm { get; set; }
    public Guid? CollegeId { get; set; }
    public bool? IsActive { get; set; }
}
