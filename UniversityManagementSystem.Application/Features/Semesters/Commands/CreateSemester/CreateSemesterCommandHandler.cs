using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniversityManagementSystem.Application.Contracts.Persistence;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities.Academic;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Semesters.Commands.CreateSemester;

public class CreateSemesterCommandHandler : IRequestHandler<CreateSemesterCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateSemesterCommandHandler> _logger;

    public CreateSemesterCommandHandler(IApplicationDbContext context, ILogger<CreateSemesterCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateSemesterCommand request, CancellationToken cancellationToken)
    {
        var existingSemester = await _context.Semesters
            .FirstOrDefaultAsync(s => s.Code == request.Code, cancellationToken);

        if (existingSemester != null)
        {
            return Result<Guid>.Failure("Semester code already exists");
        }

        var semester = new Semester
        {
            Name = request.Name,
            Code = request.Code,
            Type = request.Type,
            AcademicYear = request.AcademicYear,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            RegistrationStartDate = request.RegistrationStartDate,
            RegistrationEndDate = request.RegistrationEndDate,
            AddDropDeadline = request.AddDropDeadline,
            WithdrawalDeadline = request.WithdrawalDeadline,
            Status = DetermineSemesterStatus(request.RegistrationStartDate, request.RegistrationEndDate, request.StartDate, request.EndDate),
            IsActive = true
        };

        _context.Semesters.Add(semester);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Semester {SemesterName} created with ID {SemesterId}", semester.Name, semester.Id);

        return Result<Guid>.Success(semester.Id, "Semester created successfully");
    }

    private static SemesterStatus DetermineSemesterStatus(DateTime regStart, DateTime regEnd, DateTime start, DateTime end)
    {
        var now = DateTime.UtcNow;
        
        if (now < regStart) return SemesterStatus.Upcoming;
        if (now >= regStart && now <= regEnd) return SemesterStatus.RegistrationOpen;
        if (now > regEnd && now < start) return SemesterStatus.Upcoming;
        if (now >= start && now <= end) return SemesterStatus.InProgress;
        return SemesterStatus.Completed;
    }
}
