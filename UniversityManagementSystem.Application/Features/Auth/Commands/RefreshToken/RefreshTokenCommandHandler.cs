using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UniversityManagementSystem.Application.Common.Interfaces;
using UniversityManagementSystem.Application.DTOs.Auth;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<LoginResponse>>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationDbContext _context;

        public RefreshTokenCommandHandler(
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            IApplicationDbContext context)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<Result<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Result<LoginResponse>.Failure("Invalid token");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || !user.IsActive)
                return Result<LoginResponse>.Failure("User not found or inactive");

            var storedRefreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && rt.UserId == user.Id, cancellationToken);

            if (storedRefreshToken == null || storedRefreshToken.IsExpired || storedRefreshToken.Revoked != null)
                return Result<LoginResponse>.Failure("Invalid refresh token");

            // Revoke old refresh token
            storedRefreshToken.Revoked = DateTime.UtcNow;
            _context.RefreshTokens.Update(storedRefreshToken);

            // Generate new tokens
            var authResponse = await _tokenService.GenerateTokenAsync(user);

            // Save new refresh token
            var newRefreshToken = new RefreshToken
            {
                Token = authResponse.RefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };

            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<LoginResponse>.Success(authResponse);
        }
    }
}
