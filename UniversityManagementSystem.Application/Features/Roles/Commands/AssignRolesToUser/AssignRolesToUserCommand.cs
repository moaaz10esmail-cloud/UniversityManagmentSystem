using MediatR;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Roles.Commands.AssignRolesToUser;

public class AssignRolesToUserCommand : IRequest<Result>
{
    public Guid UserId { get; set; }
    public List<string> Roles { get; set; } = new();
}
