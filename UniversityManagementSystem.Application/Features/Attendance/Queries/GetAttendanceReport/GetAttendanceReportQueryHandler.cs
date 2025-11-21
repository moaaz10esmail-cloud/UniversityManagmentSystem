using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Attendance;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Attendance.Queries.GetAttendanceReport;

public class GetAttendanceReportQueryHandler : IRequestHandler<GetAttendanceReportQuery, Result<AttendanceReportDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAttendanceReportQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<AttendanceReportDto>> Handle(GetAttendanceReportQuery request, CancellationToken cancellationToken)
    {
        var courseOffering = await _context.CourseOfferings
            .Include(co => co.Course)
            .FirstOrDefaultAsync(co => co.Id == request.CourseOfferingId, cancellationToken);

        if (courseOffering == null)
        {
            return Result<AttendanceReportDto>.Failure("Course offering not found");
        }

        var query = _context.Attendances
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .Where(a => a.CourseOfferingId == request.CourseOfferingId);

        if (request.FromDate.HasValue)
        {
            query = query.Where(a => a.AttendanceDate >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            query = query.Where(a => a.AttendanceDate <= request.ToDate.Value);
        }

        var attendances = await query.ToListAsync(cancellationToken);

        var studentGroups = attendances.GroupBy(a => a.StudentId);
        var totalSessions = attendances.Select(a => a.AttendanceDate.Date).Distinct().Count();

        var report = new AttendanceReportDto
        {
            CourseOfferingId = request.CourseOfferingId,
            CourseCode = courseOffering.Course.Code,
            CourseName = courseOffering.Course.Name,
            FromDate = request.FromDate ?? attendances.Min(a => a.AttendanceDate),
            ToDate = request.ToDate ?? attendances.Max(a => a.AttendanceDate),
            TotalSessions = totalSessions
        };

        foreach (var group in studentGroups)
        {
            var studentAttendances = group.ToList();
            var presentCount = studentAttendances.Count(a => a.Status == AttendanceStatus.Present);
            var absentCount = studentAttendances.Count(a => a.Status == AttendanceStatus.Absent);
            var lateCount = studentAttendances.Count(a => a.Status == AttendanceStatus.Late);
            var excusedCount = studentAttendances.Count(a => a.Status == AttendanceStatus.Excused);

            var summary = new StudentAttendanceSummaryDto
            {
                StudentId = group.Key,
                StudentName = $"{studentAttendances.First().Student.User.FirstName} {studentAttendances.First().Student.User.LastName}",
                PresentCount = presentCount,
                AbsentCount = absentCount,
                LateCount = lateCount,
                ExcusedCount = excusedCount,
                AttendancePercentage = totalSessions > 0 ? (decimal)(presentCount + lateCount) / totalSessions * 100 : 0
            };

            report.StudentSummaries.Add(summary);
        }

        return Result<AttendanceReportDto>.Success(report);
    }
}
