using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Dashboard.Queries.GetDashboardStats;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ReportsController : BaseApiController
{
    [HttpGet("dashboard/stats")]
    public async Task<IActionResult> GetDashboardStats()
    {
        var result = await Mediator.Send(new GetDashboardStatsQuery());
        return HandleResult(result);
    }

    [HttpGet("academic/enrollment-stats")]
    public async Task<IActionResult> GetEnrollmentStats([FromQuery] Guid? semesterId = null)
    {
        // TODO: Implement GetEnrollmentStatsQuery
        return Ok(new { message = "Enrollment stats endpoint - to be implemented" });
    }

    [HttpGet("financial/revenue-report")]
    public async Task<IActionResult> GetRevenueReport([FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null)
    {
        // TODO: Implement GetRevenueReportQuery
        return Ok(new { message = "Revenue report endpoint - to be implemented" });
    }

    [HttpGet("attendance/summary")]
    public async Task<IActionResult> GetAttendanceSummary([FromQuery] Guid? departmentId = null, [FromQuery] Guid? semesterId = null)
    {
        // TODO: Implement GetAttendanceSummaryQuery
        return Ok(new { message = "Attendance summary endpoint - to be implemented" });
    }
}
