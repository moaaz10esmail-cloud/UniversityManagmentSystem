using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Semesters.Commands.CreateSemester;
using UniversityManagementSystem.Application.Features.Semesters.Queries.GetCurrentSemester;
using UniversityManagementSystem.Application.Features.Semesters.Queries.GetSemesters;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class SemestersController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetSemesters([FromQuery] GetSemestersQuery query)
    {
        var result = await Mediator.Send(query);
        return HandlePagedResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSemester(CreateSemesterCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("current")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCurrentSemester()
    {
        var result = await Mediator.Send(new GetCurrentSemesterQuery());
        return HandleResult(result);
    }
}
