using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.HR.Commands.CreateStaff;
using UniversityManagementSystem.Application.Features.HR.Commands.RequestLeave;
using UniversityManagementSystem.Application.Features.Shared.Queries;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,HR")]
public class HRController : BaseApiController
{
    [HttpPost("staff")]
    public async Task<IActionResult> CreateStaff(CreateStaffCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("leave/request")]
    [Authorize]
    public async Task<IActionResult> RequestLeave(RequestLeaveCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("staff/{staffId}/attendance")]
    public async Task<IActionResult> GetStaffAttendance(Guid staffId)
    {
        // TODO: Implement GetStaffAttendanceQuery
        return Ok(new { message = "Get staff attendance endpoint - to be implemented" });
    }

    [HttpGet("staff/{staffId}")]
    public async Task<IActionResult> GetStaff(Guid staffId)
    {
        var result = await Mediator.Send(new GetStaffQuery { StaffId = staffId });
        return HandleResult(result);
    }

    [HttpGet("leave/requests")]
    public async Task<IActionResult> GetLeaveRequests([FromQuery] string? status = null)
    {
        var result = await Mediator.Send(new GetLeaveRequestsQuery { Status = status });
        return HandleResult(result);
    }

    [HttpPut("leave/requests/{requestId}/approve")]
    public async Task<IActionResult> ApproveLeaveRequest(Guid requestId)
    {
        var result = await Mediator.Send(new ApproveLeaveRequestCommand { RequestId = requestId });
        return HandleResult(result);
    }
}
