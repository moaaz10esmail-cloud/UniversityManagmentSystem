using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Attendance;

namespace UniversityManagementSystem.Application.Features.Attendance.Queries.GetAttendanceReport;

public class GetAttendanceReportQuery : IRequest<Result<AttendanceReportDto>>
{
    public Guid CourseOfferingId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
