using MediatR;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.Logout;

public class LogoutCommand : IRequest<Result>
{
    public string RefreshToken { get; set; } = string.Empty;
}
