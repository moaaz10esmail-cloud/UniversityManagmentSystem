using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Attendance;

namespace UniversityManagementSystem.Application.Features.Attendance.Queries.GetStudentAttendance;

public class GetStudentAttendanceQuery : IRequest<Result<List<AttendanceDto>>>
{
    public Guid StudentId { get; set; }
    public Guid? CourseOfferingId { get; set; }
}
