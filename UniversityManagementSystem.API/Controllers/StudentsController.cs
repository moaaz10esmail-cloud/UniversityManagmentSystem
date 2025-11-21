using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystem.API.Controllers.Base;

namespace UniversityManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class StudentsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllStudents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
    {
        // TODO: Implement GetAllStudentsQuery with pagination
        return Ok(new { message = "Get all students endpoint - to be implemented", pageNumber, pageSize });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(Guid id)
    {
        // TODO: Implement GetStudentByIdQuery
        return Ok(new { message = $"Get student {id} endpoint - to be implemented" });
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent()
    {
        // TODO: Implement CreateStudentCommand
        return Ok(new { message = "Create student endpoint - to be implemented" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(Guid id)
    {
        // TODO: Implement UpdateStudentCommand
        return Ok(new { message = $"Update student {id} endpoint - to be implemented" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        // TODO: Implement DeleteStudentCommand
        return Ok(new { message = $"Delete student {id} endpoint - to be implemented" });
    }

    [HttpGet("{id}/academic-info")]
    public async Task<IActionResult> GetStudentAcademicInfo(Guid id)
    {
        // TODO: Implement GetStudentAcademicInfoQuery
        return Ok(new { message = $"Get student {id} academic info endpoint - to be implemented" });
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStudentStatus(Guid id, [FromQuery] string status)
    {
        // TODO: Implement UpdateStudentStatusCommand
        return Ok(new { message = $"Update student {id} status to {status} endpoint - to be implemented" });
    }
}
