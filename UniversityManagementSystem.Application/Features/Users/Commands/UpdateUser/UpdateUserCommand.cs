using MediatR;
using UniversityManagementSystem.Application.DTOs.Users;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Result>
{
    public Guid UserId { get; set; }
    public UpdateUserDto User { get; set; } = null!;
}
