using FluentValidation;

namespace UniversityManagementSystem.Application.Features.Colleges.Commands.CreateCollege;

public class CreateCollegeCommandValidator : AbstractValidator<CreateCollegeCommand>
{
    public CreateCollegeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("College name is required")
            .MaximumLength(100).WithMessage("College name must not exceed 100 characters");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("College code is required")
            .MaximumLength(20).WithMessage("College code must not exceed 20 characters")
            .Matches("^[A-Z0-9]+$").WithMessage("College code must contain only uppercase letters and numbers");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.DeanName)
            .MaximumLength(100).WithMessage("Dean name must not exceed 100 characters");

        RuleFor(x => x.ContactEmail)
            .EmailAddress().WithMessage("Invalid email format")
            .When(x => !string.IsNullOrEmpty(x.ContactEmail));

        RuleFor(x => x.ContactPhone)
            .MaximumLength(20).WithMessage("Contact phone must not exceed 20 characters");
    }
}
