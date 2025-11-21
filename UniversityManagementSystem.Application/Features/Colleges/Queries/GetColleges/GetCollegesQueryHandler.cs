using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Contracts.Persistence;
using UniversityManagementSystem.Application.DTOs.Academic;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Colleges.Queries.GetColleges;

public class GetCollegesQueryHandler : IRequestHandler<GetCollegesQuery, Result<PagedList<CollegeDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCollegesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<CollegeDto>>> Handle(GetCollegesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Colleges.AsQueryable();

        // Apply filters
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(c =>
                c.Name.Contains(request.SearchTerm) ||
                c.Code.Contains(request.SearchTerm) ||
                (c.Description != null && c.Description.Contains(request.SearchTerm)));
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(c => c.IsActive == request.IsActive.Value);
        }

        // Order by name
        query = query.OrderBy(c => c.Name);

        var totalCount = await query.CountAsync(cancellationToken);

        var colleges = await query
            .Include(c => c.Departments)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var collegeDtos = _mapper.Map<List<CollegeDto>>(colleges);
        var pagedList = new PagedList<CollegeDto>(collegeDtos, totalCount, request.PageNumber, request.PageSize);

        return Result<PagedList<CollegeDto>>.Success(pagedList);
    }
}
