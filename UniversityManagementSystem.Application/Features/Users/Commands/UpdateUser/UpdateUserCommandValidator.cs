using FluentValidation;

namespace UniversityManagementSystem.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required");

        RuleFor(x => x.User.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters");

        RuleFor(x => x.User.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters");

        RuleFor(x => x.User.NationalId)
            .MaximumLength(20).WithMessage("National ID must not exceed 20 characters");

        RuleFor(x => x.User.DateOfBirth)
            .LessThan(DateTime.Now.AddYears(-16)).WithMessage("User must be at least 16 years old")
            .When(x => x.User.DateOfBirth.HasValue);
    }
}
