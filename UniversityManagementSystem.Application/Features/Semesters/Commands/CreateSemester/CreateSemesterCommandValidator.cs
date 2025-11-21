using FluentValidation;

namespace UniversityManagementSystem.Application.Features.Semesters.Commands.CreateSemester;

public class CreateSemesterCommandValidator : AbstractValidator<CreateSemesterCommand>
{
    public CreateSemesterCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Semester name is required")
            .MaximumLength(100).WithMessage("Semester name must not exceed 100 characters");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Semester code is required")
            .MaximumLength(20).WithMessage("Semester code must not exceed 20 characters")
            .Matches("^[A-Z0-9]+$").WithMessage("Semester code must contain only uppercase letters and numbers");

        RuleFor(x => x.AcademicYear)
            .GreaterThan(2000).WithMessage("Academic year must be greater than 2000")
            .LessThanOrEqualTo(2100).WithMessage("Academic year must not exceed 2100");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required")
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date");

        RuleFor(x => x.RegistrationStartDate)
            .NotEmpty().WithMessage("Registration start date is required");

        RuleFor(x => x.RegistrationEndDate)
            .NotEmpty().WithMessage("Registration end date is required")
            .GreaterThan(x => x.RegistrationStartDate).WithMessage("Registration end date must be after registration start date")
            .LessThanOrEqualTo(x => x.StartDate).WithMessage("Registration must end before semester starts");

        RuleFor(x => x.AddDropDeadline)
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Add/Drop deadline must be after semester starts")
            .LessThanOrEqualTo(x => x.EndDate).WithMessage("Add/Drop deadline must be before semester ends");

        RuleFor(x => x.WithdrawalDeadline)
            .GreaterThanOrEqualTo(x => x.AddDropDeadline).WithMessage("Withdrawal deadline must be after Add/Drop deadline")
            .LessThanOrEqualTo(x => x.EndDate).WithMessage("Withdrawal deadline must be before semester ends");
    }
}
