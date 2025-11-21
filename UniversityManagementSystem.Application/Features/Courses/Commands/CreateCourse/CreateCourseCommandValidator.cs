using FluentValidation;

namespace UniversityManagementSystem.Application.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Course code is required")
            .MaximumLength(20).WithMessage("Course code must not exceed 20 characters")
            .Matches("^[A-Z0-9]+$").WithMessage("Course code must contain only uppercase letters and numbers");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Course name is required")
            .MaximumLength(200).WithMessage("Course name must not exceed 200 characters");

        RuleFor(x => x.CreditHours)
            .GreaterThan(0).WithMessage("Credit hours must be greater than 0")
            .LessThanOrEqualTo(10).WithMessage("Credit hours must not exceed 10");

        RuleFor(x => x.PracticalHours)
            .GreaterThanOrEqualTo(0).WithMessage("Practical hours cannot be negative");

        RuleFor(x => x.TheoreticalHours)
            .GreaterThanOrEqualTo(0).WithMessage("Theoretical hours cannot be negative");

        RuleFor(x => x.DepartmentId)
            .NotEmpty().WithMessage("Department ID is required");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");
    }
}
