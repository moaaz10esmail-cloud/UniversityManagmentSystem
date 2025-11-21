using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Registration.Commands.DropCourse;
using UniversityManagementSystem.Application.Features.Registration.Commands.RegisterForCourses;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RegistrationController : BaseApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterForCourses(RegisterForCoursesCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("drop")]
    public async Task<IActionResult> DropCourse(DropCourseCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}
