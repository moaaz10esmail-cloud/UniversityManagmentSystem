using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Contracts.Persistence;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Academic;

namespace UniversityManagementSystem.Application.Features.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateDepartmentCommandHandler> _logger;

    public CreateDepartmentCommandHandler(IApplicationDbContext context, ILogger<CreateDepartmentCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var college = await _context.Colleges
            .FirstOrDefaultAsync(c => c.Id == request.CollegeId, cancellationToken);

        if (college == null)
        {
            return Result<Guid>.Failure("College not found");
        }

        var existingDepartment = await _context.Departments
            .FirstOrDefaultAsync(d => d.Code == request.Code && d.CollegeId == request.CollegeId, cancellationToken);

        if (existingDepartment != null)
        {
            return Result<Guid>.Failure("Department code already exists in this college");
        }

        var department = new Department
        {
            Name = request.Name,
            Code = request.Code,
            Description = request.Description,
            HeadOfDepartment = request.HeadOfDepartment,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone,
            CollegeId = request.CollegeId,
            IsActive = true
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Department {DepartmentName} created in college {CollegeName}", 
            department.Name, college.Name);

        return Result<Guid>.Success(department.Id, "Department created successfully");
    }
}
