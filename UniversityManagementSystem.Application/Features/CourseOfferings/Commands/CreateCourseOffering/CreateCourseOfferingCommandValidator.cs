using FluentValidation;

namespace UniversityManagementSystem.Application.Features.CourseOfferings.Commands.CreateCourseOffering;

public class CreateCourseOfferingCommandValidator : AbstractValidator<CreateCourseOfferingCommand>
{
    public CreateCourseOfferingCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("Course ID is required");

        RuleFor(x => x.SemesterId)
            .NotEmpty().WithMessage("Semester ID is required");

        RuleFor(x => x.Section)
            .NotEmpty().WithMessage("Section is required")
            .MaximumLength(10).WithMessage("Section must not exceed 10 characters");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than 0")
            .LessThanOrEqualTo(500).WithMessage("Capacity must not exceed 500");
    }
}
