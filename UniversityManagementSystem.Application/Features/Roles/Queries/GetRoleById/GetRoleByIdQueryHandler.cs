using MediatR;
using Microsoft.AspNetCore.Identity;
using UniversityManagementSystem.Application.DTOs.Roles;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Features.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetRoleByIdQueryHandler(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        
        if (role == null)
        {
            return Result<RoleDto>.Failure("Role not found");
        }

        var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);

        var roleDto = new RoleDto
        {
            Id = role.Id,
            Name = role.Name!,
            Description = role.Description,
            CreatedAt = role.CreatedAt,
            CreatedBy = role.CreatedBy,
            UsersCount = usersInRole.Count
        };

        return Result<RoleDto>.Success(roleDto);
    }
}
