using MediatR;
using UniversityManagementSystem.Application.DTOs.Roles;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Roles.Queries.GetRoleById;

public class GetRoleByIdQuery : IRequest<Result<RoleDto>>
{
    public Guid RoleId { get; set; }
}
