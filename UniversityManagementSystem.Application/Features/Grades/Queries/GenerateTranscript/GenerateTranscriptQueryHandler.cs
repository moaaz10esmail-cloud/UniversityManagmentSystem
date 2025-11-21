using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Grades;
using UniversityManagementSystem.Application.Interfaces;

namespace UniversityManagementSystem.Application.Features.Grades.Queries.GenerateTranscript;

public class GenerateTranscriptQueryHandler : IRequestHandler<GenerateTranscriptQuery, Result<TranscriptDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GenerateTranscriptQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<TranscriptDto>> Handle(GenerateTranscriptQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .Include(s => s.User)
            .Include(s => s.Department)
                .ThenInclude(d => d.College)
            .FirstOrDefaultAsync(s => s.Id == request.StudentId, cancellationToken);

        if (student == null)
        {
            return Result<TranscriptDto>.Failure("Student not found");
        }

        var grades = await _context.Grades
            .Include(g => g.StudentRegistration)
            .Include(g => g.CourseOffering)
                .ThenInclude(co => co.Course)
            .Include(g => g.CourseOffering)
                .ThenInclude(co => co.Semester)
            .Where(g => g.StudentRegistration.StudentId == request.StudentId)
            .OrderBy(g => g.CourseOffering.Semester.StartDate)
            .ToListAsync(cancellationToken);

        var transcript = new TranscriptDto
        {
            StudentId = student.Id,
            StudentName = $"{student.User.FirstName} {student.User.LastName}",
            StudentIdNumber = student.StudentId,
            Department = student.Department.Name,
            College = student.Department.College.Name,
            EnrollmentDate = student.EnrollmentDate,
            GeneratedDate = DateTime.UtcNow,
            GeneratedBy = "System",
            CumulativeGPA = student.CGPA ?? 0,
            TotalCreditsAttempted = student.TotalCredits,
            TotalCreditsEarned = student.TotalCredits
        };

        var semesterGroups = grades.GroupBy(g => new
        {
            g.CourseOffering.SemesterId,
            g.CourseOffering.Semester.Name,
            g.CourseOffering.Semester.Code,
            g.CourseOffering.Semester.StartDate,
            g.CourseOffering.Semester.EndDate
        });

        foreach (var semesterGroup in semesterGroups)
        {
            var semesterDto = new TranscriptSemesterDto
            {
                SemesterName = semesterGroup.Key.Name,
                SemesterCode = semesterGroup.Key.Code,
                StartDate = semesterGroup.Key.StartDate,
                EndDate = semesterGroup.Key.EndDate,
                SemesterGPA = semesterGroup.Average(g => g.GradePoints)
            };

            foreach (var grade in semesterGroup)
            {
                semesterDto.Courses.Add(new TranscriptCourseDto
                {
                    CourseCode = grade.CourseOffering.Course.Code,
                    CourseName = grade.CourseOffering.Course.Name,
                    CreditHours = grade.CourseOffering.Course.CreditHours,
                    LetterGrade = grade.LetterGrade,
                    GradePoints = grade.GradePoints,
                    TotalScore = grade.TotalScore
                });
            }

            transcript.Semesters.Add(semesterDto);
        }

        return Result<TranscriptDto>.Success(transcript);
    }
}
