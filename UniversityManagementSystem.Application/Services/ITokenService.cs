using System.Security.Claims;
using UniversityManagementSystem.Application.DTOs.Auth;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Services;

public interface ITokenService
{
    Task<AuthResponse> GenerateTokenAsync(ApplicationUser user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
