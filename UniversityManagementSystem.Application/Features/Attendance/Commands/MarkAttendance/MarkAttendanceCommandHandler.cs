using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Attendance.Commands.MarkAttendance;

public class MarkAttendanceCommandHandler : IRequestHandler<MarkAttendanceCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public MarkAttendanceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(MarkAttendanceCommand request, CancellationToken cancellationToken)
    {
        var courseOffering = await _context.CourseOfferings
            .FirstOrDefaultAsync(co => co.Id == request.CourseOfferingId, cancellationToken);

        if (courseOffering == null)
        {
            return Result.Failure("Course offering not found");
        }

        foreach (var studentAttendance in request.StudentAttendances)
        {
            if (!Enum.TryParse<AttendanceStatus>(studentAttendance.Status, out var status))
            {
                continue;
            }

            var attendance = new Core.Entities.Academic.Attendance
            {
                StudentId = studentAttendance.StudentId,
                CourseOfferingId = request.CourseOfferingId,
                AttendanceDate = request.AttendanceDate,
                Status = status,
                TimeIn = studentAttendance.TimeIn,
                Notes = studentAttendance.Notes
            };

            _context.Attendances.Add(attendance);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success($"Attendance marked for {request.StudentAttendances.Count} students");
    }
}
