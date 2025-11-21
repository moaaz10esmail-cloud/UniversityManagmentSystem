using MediatR;
using UniversityManagementSystem.Application.DTOs.Auth;
using UniversityManagementSystem.Core.Common;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<Result<LoginResponse>>
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
