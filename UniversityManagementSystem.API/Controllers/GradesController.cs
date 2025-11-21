using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;
using UniversityManagementSystem.Application.Features.Grades.Queries.GetStudentGrades;
using UniversityManagementSystem.Application.Features.Grades.Commands.SubmitGrades;
using UniversityManagementSystem.Application.Features.Grades.Queries.GenerateTranscript;
using UniversityManagementSystem.Application.Features.Shared.Queries;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GradesController : BaseApiController
{
    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetStudentGrades(Guid studentId, [FromQuery] Guid? semesterId = null)
    {
        var result = await Mediator.Send(new GetStudentGradesQuery { StudentId = studentId, SemesterId = semesterId });
        return HandleResult(result);
    }

    [HttpGet("course/{courseOfferingId}")]
    [Authorize(Roles = "Admin,Lecturer")]
    public async Task<IActionResult> GetCourseGrades(Guid courseOfferingId)
    {
        // TODO: Implement GetCourseGradesQuery
        return Ok(new { message = "Get course grades endpoint - to be implemented" });
    }

    [HttpPost("submit")]
    [Authorize(Roles = "Admin,Lecturer")]
    public async Task<IActionResult> SubmitGrades(SubmitGradesCommand command)
    {
        var result = await Mediator.Send(command);
        return HandleResult(result);
    }

    [HttpGet("transcript/{studentId}")]
    public async Task<IActionResult> GenerateTranscript(Guid studentId, [FromQuery] bool includeCurrentSemester = true)
    {
        var result = await Mediator.Send(new GenerateTranscriptQuery { StudentId = studentId, IncludeCurrentSemester = includeCurrentSemester });
        return HandleResult(result);
    }

    [HttpPost("calculate-gpa")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CalculateGPA()
    {
        // TODO: Implement CalculateGPACommand
        return Ok(new { message = "Calculate GPA endpoint - to be implemented" });
    }
}
