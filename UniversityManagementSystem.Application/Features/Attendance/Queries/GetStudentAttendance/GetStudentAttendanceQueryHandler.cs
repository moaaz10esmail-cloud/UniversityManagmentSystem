using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Attendance;
using UniversityManagementSystem.Application.Interfaces;

namespace UniversityManagementSystem.Application.Features.Attendance.Queries.GetStudentAttendance;

public class GetStudentAttendanceQueryHandler : IRequestHandler<GetStudentAttendanceQuery, Result<List<AttendanceDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentAttendanceQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<AttendanceDto>>> Handle(GetStudentAttendanceQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Attendances
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .Include(a => a.CourseOffering)
                .ThenInclude(co => co.Course)
            .Where(a => a.StudentId == request.StudentId);

        if (request.CourseOfferingId.HasValue)
        {
            query = query.Where(a => a.CourseOfferingId == request.CourseOfferingId.Value);
        }

        var attendances = await query
            .OrderByDescending(a => a.AttendanceDate)
            .ToListAsync(cancellationToken);

        var attendanceDtos = _mapper.Map<List<AttendanceDto>>(attendances);
        return Result<List<AttendanceDto>>.Success(attendanceDtos);
    }
}
