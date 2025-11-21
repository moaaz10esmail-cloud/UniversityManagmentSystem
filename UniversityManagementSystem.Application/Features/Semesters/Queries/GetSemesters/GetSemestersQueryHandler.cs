using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Contracts.Persistence;
using UniversityManagementSystem.Application.DTOs.Academic;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Semesters.Queries.GetSemesters;

public class GetSemestersQueryHandler : IRequestHandler<GetSemestersQuery, Result<PagedList<SemesterDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSemestersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<SemesterDto>>> Handle(GetSemestersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Semesters.AsQueryable();

        if (request.Type.HasValue)
        {
            query = query.Where(s => s.Type == request.Type.Value);
        }

        if (request.AcademicYear.HasValue)
        {
            query = query.Where(s => s.AcademicYear == request.AcademicYear.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(s => s.Status == request.Status.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(s => s.IsActive == request.IsActive.Value);
        }

        query = query.OrderByDescending(s => s.StartDate);

        var totalCount = await query.CountAsync(cancellationToken);

        var semesters = await query
            .Include(s => s.CourseOfferings)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var semesterDtos = _mapper.Map<List<SemesterDto>>(semesters);
        var pagedList = new PagedList<SemesterDto>(semesterDtos, totalCount, request.PageNumber, request.PageSize);

        return Result<PagedList<SemesterDto>>.Success(pagedList);
    }
}
