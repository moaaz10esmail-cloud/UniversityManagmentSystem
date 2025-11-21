using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Entities.HR;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.HR.Commands.RequestLeave;

public class RequestLeaveCommandHandler : IRequestHandler<RequestLeaveCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public RequestLeaveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(RequestLeaveCommand request, CancellationToken cancellationToken)
    {
        var staff = await _context.Staffs
            .FirstOrDefaultAsync(s => s.Id == request.StaffId, cancellationToken);

        if (staff == null)
        {
            return Result.Failure("Staff member not found");
        }

        if (request.EndDate < request.StartDate)
        {
            return Result.Failure("End date must be after start date");
        }

        var totalDays = (request.EndDate - request.StartDate).Days + 1;

        var leaveRequest = new LeaveRequest
        {
            StaffId = request.StaffId,
            Type = request.Type,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalDays = totalDays,
            Reason = request.Reason,
            Status = LeaveStatus.Pending
        };

        _context.LeaveRequests.Add(leaveRequest);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success($"Leave request submitted successfully for {totalDays} day(s)");
    }
}
