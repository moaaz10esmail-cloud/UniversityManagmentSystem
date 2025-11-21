using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Core.Entities.Academic;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Grades.Commands.SubmitGrades;

public class SubmitGradesCommandHandler : IRequestHandler<SubmitGradesCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public SubmitGradesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(SubmitGradesCommand request, CancellationToken cancellationToken)
    {
        var courseOffering = await _context.CourseOfferings
            .FirstOrDefaultAsync(co => co.Id == request.CourseOfferingId, cancellationToken);

        if (courseOffering == null)
        {
            return Result.Failure("Course offering not found");
        }

        foreach (var gradeSubmission in request.Grades)
        {
            var existingGrade = await _context.Grades
                .FirstOrDefaultAsync(g => g.StudentRegistrationId == gradeSubmission.StudentRegistrationId, cancellationToken);

            if (existingGrade != null)
            {
                existingGrade.TotalScore = gradeSubmission.TotalScore;
                existingGrade.LetterGrade = gradeSubmission.LetterGrade;
                existingGrade.GradePoints = gradeSubmission.GradePoints;
                existingGrade.Status = GradeStatus.Final;
            }
            else
            {
                var grade = new Grade
                {
                    StudentRegistrationId = gradeSubmission.StudentRegistrationId,
                    CourseOfferingId = request.CourseOfferingId,
                    TotalScore = gradeSubmission.TotalScore,
                    LetterGrade = gradeSubmission.LetterGrade,
                    GradePoints = gradeSubmission.GradePoints,
                    Status = GradeStatus.Final
                };
                _context.Grades.Add(grade);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success($"Grades submitted successfully for {request.Grades.Count} students");
    }
}
