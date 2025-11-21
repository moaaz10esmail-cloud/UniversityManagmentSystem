using MediatR;
using UniversityManagementSystem.Application.DTOs.Common;
using UniversityManagementSystem.Application.DTOs.Users;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Users.Queries.GetUsers;

public class GetUsersQuery : PaginationRequest, IRequest<Result<PagedList<UserDto>>>
{
    public string? SearchTerm { get; set; }
    public UserType? UserType { get; set; }
    public bool? IsActive { get; set; }
    public Guid? DepartmentId { get; set; }
}
