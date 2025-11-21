using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.DTOs.Users;
using UniversityManagementSystem.Application.Features.Users.Commands.DeleteUser;
using UniversityManagementSystem.Application.Features.Users.Commands.UpdateUser;
using UniversityManagementSystem.Application.Features.Users.Queries.GetUserById;
using UniversityManagementSystem.Application.Features.Users.Queries.GetUsers;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
    {
        var result = await Mediator.Send(query);
        return HandlePagedResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var result = await Mediator.Send(new GetUserByIdQuery { UserId = id });
        return HandleResult(result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto user)
    {
        var result = await Mediator.Send(new UpdateUserCommand { UserId = id, User = user });
        return HandleResult(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var result = await Mediator.Send(new DeleteUserCommand { UserId = id });
        return HandleResult(result);
    }
}
