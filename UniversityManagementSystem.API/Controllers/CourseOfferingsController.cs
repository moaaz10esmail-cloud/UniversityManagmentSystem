using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.CourseOfferings.Commands.CreateCourseOffering;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class CourseOfferingsController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateCourseOffering(CreateCourseOfferingCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }
}
