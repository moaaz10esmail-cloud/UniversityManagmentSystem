using MediatR;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<Result>
{
    public Guid UserId { get; set; }
}
