using MediatR;
using UniversityManagementSystem.Application.DTOs.Roles;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<Result<List<RoleDto>>>
{
    public bool IncludeUserCount { get; set; } = true;
}
