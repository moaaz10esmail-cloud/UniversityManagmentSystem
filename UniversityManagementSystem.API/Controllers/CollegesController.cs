using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Colleges.Commands.CreateCollege;
using UniversityManagementSystem.Application.Features.Colleges.Queries.GetColleges;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class CollegesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetColleges([FromQuery] GetCollegesQuery query)
    {
        var result = await Mediator.Send(query);
        return HandlePagedResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCollege(CreateCollegeCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}
