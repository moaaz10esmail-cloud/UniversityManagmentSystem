using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Grades;
using UniversityManagementSystem.Application.Interfaces;

namespace UniversityManagementSystem.Application.Features.Grades.Queries.GetStudentGrades;

public class GetStudentGradesQueryHandler : IRequestHandler<GetStudentGradesQuery, Result<List<GradeDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentGradesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<GradeDto>>> Handle(GetStudentGradesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Grades
            .Include(g => g.StudentRegistration)
                .ThenInclude(sr => sr.Student)
                    .ThenInclude(s => s.User)
            .Include(g => g.CourseOffering)
                .ThenInclude(co => co.Course)
            .Where(g => g.StudentRegistration.StudentId == request.StudentId);

        if (request.SemesterId.HasValue)
        {
            query = query.Where(g => g.CourseOffering.SemesterId == request.SemesterId.Value);
        }

        var grades = await query.ToListAsync(cancellationToken);
        var gradeDtos = _mapper.Map<List<GradeDto>>(grades);

        return Result<List<GradeDto>>.Success(gradeDtos);
    }
}
