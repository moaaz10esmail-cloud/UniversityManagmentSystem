using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.HR;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Entities.HR;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.HR.Commands.CreateStaff;

public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, Result<StaffDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateStaffCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<StaffDto>> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
        if (user == null)
        {
            return Result<StaffDto>.Failure("User not found");
        }

        var department = await _context.Departments.FindAsync(new object[] { request.DepartmentId }, cancellationToken);
        if (department == null)
        {
            return Result<StaffDto>.Failure("Department not found");
        }

        // Generate employee ID
        var staffCount = await _context.Staffs.CountAsync(cancellationToken);
        var employeeId = $"EMP-{DateTime.UtcNow:yyyy}-{(staffCount + 1):D5}";

        var staff = new Staff
        {
            UserId = request.UserId,
            EmployeeId = employeeId,
            DepartmentId = request.DepartmentId,
            Position = request.Position,
            HireDate = request.HireDate,
            Salary = request.Salary,
            Status = EmploymentStatus.Active,
            Qualifications = request.Qualifications,
            Specialization = request.Specialization
        };

        _context.Staffs.Add(staff);
        await _context.SaveChangesAsync(cancellationToken);

        var staffDto = _mapper.Map<StaffDto>(staff);
        return Result<StaffDto>.Success(staffDto, "Staff member created successfully");
    }
}
