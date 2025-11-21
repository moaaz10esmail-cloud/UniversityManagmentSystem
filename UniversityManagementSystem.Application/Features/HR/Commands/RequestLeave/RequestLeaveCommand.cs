using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.HR.Commands.RequestLeave;

public class RequestLeaveCommand : IRequest<Result>
{
    public Guid StaffId { get; set; }
    public LeaveType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
}
