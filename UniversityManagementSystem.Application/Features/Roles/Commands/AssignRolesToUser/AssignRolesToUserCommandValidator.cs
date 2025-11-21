using FluentValidation;

namespace UniversityManagementSystem.Application.Features.Roles.Commands.AssignRolesToUser;

public class AssignRolesToUserCommandValidator : AbstractValidator<AssignRolesToUserCommand>
{
    public AssignRolesToUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required");

        RuleFor(x => x.Roles)
            .NotEmpty().WithMessage("At least one role must be specified");
    }
}
