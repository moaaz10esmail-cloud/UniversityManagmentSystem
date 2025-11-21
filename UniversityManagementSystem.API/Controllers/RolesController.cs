using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.DTOs.Roles;
using UniversityManagementSystem.Application.Features.Roles.Commands.AssignRolesToUser;
using UniversityManagementSystem.Application.Features.Roles.Commands.CreateRole;
using UniversityManagementSystem.Application.Features.Roles.Queries.GetRoleById;
using UniversityManagementSystem.Application.Features.Roles.Queries.GetRoles;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class RolesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetRoles([FromQuery] bool includeUserCount = true)
    {
        var result = await Mediator.Send(new GetRolesQuery { IncludeUserCount = includeUserCount });
        return HandleResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoleById(Guid id)
    {
        var result = await Mediator.Send(new GetRoleByIdQuery { RoleId = id });
        return HandleResult(result);
    }

    [HttpPost]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> CreateRole(CreateRoleDto role)
    {
        var result = await Mediator.Send(new CreateRoleCommand { Role = role });
        return HandleResult(result);
    }

    [HttpPost("assign-roles")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> AssignRolesToUser(AssignRolesToUserCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}
