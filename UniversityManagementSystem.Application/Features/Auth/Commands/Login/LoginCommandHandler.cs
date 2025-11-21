using MediatR;
using Microsoft.AspNetCore.Identity;
using RefreshTokenEntity = UniversityManagementSystem.Core.Entities.Identity.RefreshToken;
using UniversityManagementSystem.Application.Common;
using UniversityManagementSystem.Application.DTOs.Auth;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Application.Services;
using UniversityManagementSystem.Core.Entities.Identity;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IApplicationDbContext _context;

    public LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        IApplicationDbContext context)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _context = context;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result<LoginResponse>.Failure("Invalid login attempt");

        if (!user.IsActive)
            return Result<LoginResponse>.Failure("Account is deactivated");

        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
            return Result<LoginResponse>.Failure("Invalid login attempt");

        var authResponse = await _tokenService.GenerateTokenAsync(user);

        // Save refresh token
        var refreshToken = new RefreshTokenEntity
        {
            Token = authResponse.RefreshToken,
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync(cancellationToken);

        var loginResponse = new LoginResponse
        {
            Token = authResponse.Token,
            RefreshToken = authResponse.RefreshToken,
            Expiration = authResponse.Expiration,
            UserId = authResponse.UserId,
            Email = authResponse.Email,
            FirstName = authResponse.FirstName,
            LastName = authResponse.LastName,
            UserType = authResponse.UserType,
            Roles = authResponse.Roles
        };

        return Result<LoginResponse>.Success(loginResponse);
    }
}
