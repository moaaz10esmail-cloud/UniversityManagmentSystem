using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Departments.Commands.CreateDepartment;
using UniversityManagementSystem.Application.Features.Departments.Queries.GetDepartments;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class DepartmentsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetDepartments([FromQuery] GetDepartmentsQuery query)
    {
        var result = await Mediator.Send(query);
        return HandlePagedResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(CreateDepartmentCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}
