using MediatR;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.ResendEmailConfirmation;

public class ResendEmailConfirmationCommand : IRequest<Result>
{
    public string Email { get; set; } = string.Empty;
}
