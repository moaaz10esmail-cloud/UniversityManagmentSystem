using MediatR;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Application.DTOs.Auth;
using UniversityManagementSystem.Core.Enums;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<Result<LoginResponse>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserType UserType { get; set; }
    }
}
