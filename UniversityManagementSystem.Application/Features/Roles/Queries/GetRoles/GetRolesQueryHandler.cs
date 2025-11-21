using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.DTOs.Roles;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Features.Roles.Queries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, Result<List<RoleDto>>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetRolesQueryHandler(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<Result<List<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);

        var roleDtos = new List<RoleDto>();

        foreach (var role in roles)
        {
            var roleDto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name!,
                Description = role.Description,
                CreatedAt = role.CreatedAt,
                CreatedBy = role.CreatedBy
            };

            if (request.IncludeUserCount)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
                roleDto.UsersCount = usersInRole.Count;
            }

            roleDtos.Add(roleDto);
        }

        return Result<List<RoleDto>>.Success(roleDtos);
    }
}
