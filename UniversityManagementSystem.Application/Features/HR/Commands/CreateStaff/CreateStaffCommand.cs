using MediatR;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.HR;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.HR.Commands.CreateStaff;

public class CreateStaffCommand : IRequest<Result<StaffDto>>
{
    public Guid UserId { get; set; }
    public Guid DepartmentId { get; set; }
    public StaffPosition Position { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public string? Qualifications { get; set; }
    public string? Specialization { get; set; }
}
