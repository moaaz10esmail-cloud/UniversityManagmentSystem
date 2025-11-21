using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Contracts.Persistence;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Academic;

namespace UniversityManagementSystem.Application.Features.Colleges.Commands.CreateCollege;

public class CreateCollegeCommandHandler : IRequestHandler<CreateCollegeCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateCollegeCommandHandler> _logger;

    public CreateCollegeCommandHandler(IApplicationDbContext context, ILogger<CreateCollegeCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateCollegeCommand request, CancellationToken cancellationToken)
    {
        // Check if college code already exists
        var existingCollege = await _context.Colleges
            .FirstOrDefaultAsync(c => c.Code == request.Code, cancellationToken);

        if (existingCollege != null)
        {
            return Result<Guid>.Failure("College code already exists");
        }

        var college = new College
        {
            Name = request.Name,
            Code = request.Code,
            Description = request.Description,
            DeanName = request.DeanName,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone,
            IsActive = true
        };

        _context.Colleges.Add(college);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("College {CollegeName} created with ID {CollegeId}", college.Name, college.Id);

        return Result<Guid>.Success(college.Id, "College created successfully");
    }
}
