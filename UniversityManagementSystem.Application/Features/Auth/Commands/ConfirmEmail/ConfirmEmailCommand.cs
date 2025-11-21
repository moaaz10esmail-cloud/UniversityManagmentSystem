using MediatR;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<Result>
{
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}
