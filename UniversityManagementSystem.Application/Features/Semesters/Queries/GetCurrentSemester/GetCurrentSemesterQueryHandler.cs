using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Contracts.Persistence;
using UniversityManagementSystem.Application.DTOs.Academic;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Semesters.Queries.GetCurrentSemester;

public class GetCurrentSemesterQueryHandler : IRequestHandler<GetCurrentSemesterQuery, Result<SemesterDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCurrentSemesterQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<SemesterDto>> Handle(GetCurrentSemesterQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        
        var currentSemester = await _context.Semesters
            .Where(s => s.IsActive && 
                       s.Status != SemesterStatus.Completed && 
                       s.Status != SemesterStatus.Cancelled)
            .OrderBy(s => s.StartDate)
            .FirstOrDefaultAsync(cancellationToken);

        if (currentSemester == null)
        {
            return Result<SemesterDto>.Failure("No active semester found");
        }

        var semesterDto = _mapper.Map<SemesterDto>(currentSemester);
        return Result<SemesterDto>.Success(semesterDto);
    }
}
