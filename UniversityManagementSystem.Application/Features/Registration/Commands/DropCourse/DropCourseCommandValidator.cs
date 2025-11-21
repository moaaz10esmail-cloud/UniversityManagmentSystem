using FluentValidation;

namespace UniversityManagementSystem.Application.Features.Registration.Commands.DropCourse;

public class DropCourseCommandValidator : AbstractValidator<DropCourseCommand>
{
    public DropCourseCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required");

        RuleFor(x => x.CourseOfferingId)
            .NotEmpty().WithMessage("Course offering ID is required");
    }
}
