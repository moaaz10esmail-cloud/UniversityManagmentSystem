using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Portal.Queries.GetLecturerDashboard;
using UniversityManagementSystem.Application.Features.Shared.Queries;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/lecturer-portal")]
[Authorize(Roles = "Lecturer")]
public class LecturerPortalController : BaseApiController
{
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetLecturerDashboard([FromQuery] Guid lecturerId)
    {
        var result = await Mediator.Send(new GetLecturerDashboardQuery { LecturerId = lecturerId });
        return HandleResult(result);
    }

    [HttpGet("courses/current")]
    public async Task<IActionResult> GetCurrentCourses()
    {
        // TODO: Implement GetLecturerCoursesQuery
        return Ok(new { message = "Current courses endpoint - to be implemented" });
    }

    [HttpGet("attendance/{courseOfferingId}")]
    public async Task<IActionResult> GetCourseAttendance(Guid courseOfferingId)
    {
        // TODO: Implement GetCourseAttendanceQuery
        return Ok(new { message = "Course attendance endpoint - to be implemented" });
    }

    [HttpGet("students/{courseOfferingId}")]
    public async Task<IActionResult> GetCourseStudents(Guid courseOfferingId)
    {
        // TODO: Implement GetCourseStudentsQuery
        return Ok(new { message = "Course students endpoint - to be implemented" });
    }

    [HttpPost("grades/submit")]
    public async Task<IActionResult> SubmitGrades()
    {
        // TODO: Implement SubmitGradesCommand
        return Ok(new { message = "Submit grades endpoint - to be implemented" });
    }

    [HttpGet("schedule")]
    public async Task<IActionResult> GetMySchedule()
    {
        // TODO: Implement GetLecturerScheduleQuery
        return Ok(new { message = "Lecturer schedule endpoint - to be implemented" });
    }
}
