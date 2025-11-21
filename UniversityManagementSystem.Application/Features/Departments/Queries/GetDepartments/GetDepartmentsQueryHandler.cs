using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Contracts.Persistence;
using UniversityManagementSystem.Application.DTOs.Academic;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Departments.Queries.GetDepartments;

public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, Result<PagedList<DepartmentDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDepartmentsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<DepartmentDto>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Departments.Include(d => d.College).AsQueryable();

        // Apply filters
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(d =>
                d.Name.Contains(request.SearchTerm) ||
                d.Code.Contains(request.SearchTerm) ||
                (d.Description != null && d.Description.Contains(request.SearchTerm)));
        }

        if (request.CollegeId.HasValue)
        {
            query = query.Where(d => d.CollegeId == request.CollegeId.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(d => d.IsActive == request.IsActive.Value);
        }

        query = query.OrderBy(d => d.Name);

        var totalCount = await query.CountAsync(cancellationToken);

        var departments = await query
            .Include(d => d.Courses)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var departmentDtos = _mapper.Map<List<DepartmentDto>>(departments);
        var pagedList = new PagedList<DepartmentDto>(departmentDtos, totalCount, request.PageNumber, request.PageSize);

        return Result<PagedList<DepartmentDto>>.Success(pagedList);
    }
}
