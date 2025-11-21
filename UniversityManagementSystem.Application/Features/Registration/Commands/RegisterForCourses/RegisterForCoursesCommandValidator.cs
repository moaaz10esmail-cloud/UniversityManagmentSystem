using FluentValidation;

namespace UniversityManagementSystem.Application.Features.Registration.Commands.RegisterForCourses;

public class RegisterForCoursesCommandValidator : AbstractValidator<RegisterForCoursesCommand>
{
    public RegisterForCoursesCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required");

        RuleFor(x => x.CourseOfferingIds)
            .NotEmpty().WithMessage("At least one course offering must be selected")
            .Must(x => x.Count <= 7).WithMessage("Cannot register for more than 7 courses at once");
    }
}
