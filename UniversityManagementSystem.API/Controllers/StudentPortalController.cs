using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Portal.Queries.GetStudentDashboard;
using UniversityManagementSystem.Application.Features.Registration.Queries.GetStudentSchedule;
using UniversityManagementSystem.Application.Features.Grades.Queries.GetStudentGrades;
using UniversityManagementSystem.Application.Features.Grades.Queries.GenerateTranscript;
using UniversityManagementSystem.Application.Features.Attendance.Queries.GetStudentAttendance;
using UniversityManagementSystem.Application.Features.Finance.Queries.GetFinancialSummary;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/student-portal")]
[Authorize(Roles = "Student")]
public class StudentPortalController : BaseApiController
{
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetStudentDashboard([FromQuery] Guid studentId)
    {
        var result = await Mediator.Send(new GetStudentDashboardQuery { StudentId = studentId });
        return HandleResult(result);
    }

    [HttpGet("schedule/current")]
    public async Task<IActionResult> GetCurrentSchedule([FromQuery] Guid studentId)
    {
        var result = await Mediator.Send(new GetStudentScheduleQuery { StudentId = studentId });
        return HandleResult(result);
    }

    [HttpGet("grades/current")]
    public async Task<IActionResult> GetCurrentGrades([FromQuery] Guid studentId, [FromQuery] Guid? semesterId = null)
    {
        var result = await Mediator.Send(new GetStudentGradesQuery { StudentId = studentId, SemesterId = semesterId });
        return HandleResult(result);
    }

    [HttpGet("financial/balance")]
    public async Task<IActionResult> GetFinancialBalance([FromQuery] Guid studentId)
    {
        var result = await Mediator.Send(new GetFinancialSummaryQuery { StudentId = studentId });
        return HandleResult(result);
    }

    [HttpGet("transcript")]
    public async Task<IActionResult> GetTranscript([FromQuery] Guid studentId, [FromQuery] bool includeCurrentSemester = true)
    {
        var result = await Mediator.Send(new GenerateTranscriptQuery { StudentId = studentId, IncludeCurrentSemester = includeCurrentSemester });
        return HandleResult(result);
    }

    [HttpGet("attendance")]
    public async Task<IActionResult> GetMyAttendance([FromQuery] Guid studentId, [FromQuery] Guid? courseOfferingId = null)
    {
        var result = await Mediator.Send(new GetStudentAttendanceQuery { StudentId = studentId, CourseOfferingId = courseOfferingId });
        return HandleResult(result);
    }
}
