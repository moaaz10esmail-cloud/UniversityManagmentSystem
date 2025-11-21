using MediatR;
using UniversityManagementSystem.Application.DTOs.Users;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<Result<UserDto>>
{
    public Guid UserId { get; set; }
}
