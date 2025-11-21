using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Attendance;

namespace UniversityManagementSystem.Application.Features.Attendance.Commands.MarkAttendance;

public class MarkAttendanceCommand : IRequest<Result>
{
    public Guid CourseOfferingId { get; set; }
    public DateTime AttendanceDate { get; set; }
    public List<MarkAttendanceDto> StudentAttendances { get; set; } = new();
}
