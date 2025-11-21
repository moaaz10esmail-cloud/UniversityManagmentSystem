using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Attendance.Commands.MarkAttendance;
using UniversityManagementSystem.Application.Features.Attendance.Queries.GetAttendanceReport;
using UniversityManagementSystem.Application.Features.Attendance.Queries.GetStudentAttendance;
using UniversityManagementSystem.Application.Features.Shared.Queries;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AttendanceController : BaseApiController
{
    [HttpPost("mark")]
    [Authorize(Roles = "Admin,Lecturer")]
    public async Task<IActionResult> MarkAttendance(MarkAttendanceCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("course/{courseOfferingId}")]
    public async Task<IActionResult> GetCourseAttendance(Guid courseOfferingId)
    {
        var result = await Mediator.Send(new GetCourseAttendanceQuery { CourseOfferingId = courseOfferingId });
        return HandleResult(result);
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetStudentAttendance(Guid studentId, [FromQuery] Guid? courseOfferingId = null)
    {
        var result = await Mediator.Send(new GetStudentAttendanceQuery { StudentId = studentId, CourseOfferingId = courseOfferingId });
        return HandleResult(result);
    }

    [HttpGet("report")]
    [Authorize(Roles = "Admin,Lecturer")]
    public async Task<IActionResult> GetAttendanceReport([FromQuery] Guid courseOfferingId, [FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null)
    {
        var result = await Mediator.Send(new GetAttendanceReportQuery 
        { 
            CourseOfferingId = courseOfferingId,
            FromDate = fromDate,
            ToDate = toDate
        });
        return HandleResult(result);
    }
}
