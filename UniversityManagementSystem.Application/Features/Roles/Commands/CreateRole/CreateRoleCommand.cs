using MediatR;
using UniversityManagementSystem.Application.DTOs.Roles;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommand : IRequest<Result<Guid>>
{
    public CreateRoleDto Role { get; set; } = null!;
}
