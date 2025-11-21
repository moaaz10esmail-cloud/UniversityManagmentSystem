using FluentValidation;

namespace UniversityManagementSystem.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Role.Name)
            .NotEmpty().WithMessage("Role name is required")
            .MaximumLength(50).WithMessage("Role name must not exceed 50 characters")
            .Matches("^[a-zA-Z0-9 ]+$").WithMessage("Role name can only contain letters, numbers and spaces");

        RuleFor(x => x.Role.Description)
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters");
    }
}
