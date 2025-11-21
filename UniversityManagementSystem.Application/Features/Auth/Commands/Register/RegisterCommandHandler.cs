using MediatR;
using Microsoft.AspNetCore.Identity;
using UniversityManagementSystem.Application.Common.Interfaces;
using UniversityManagementSystem.Application.DTOs.Auth;
using UniversityManagementSystem.Core.Common;
using UniversityManagementSystem.Core.Entities;

namespace UniversityManagementSystem.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<LoginResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IApplicationDbContext _context;

        public RegisterCommandHandler(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IApplicationDbContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<Result<LoginResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return Result<LoginResponse>.Failure("Email is already registered");

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserType = request.UserType,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return Result<LoginResponse>.Failure(result.Errors.Select(e => e.Description).ToArray());

            // Assign role based on user type
            var roleResult = await _userManager.AddToRoleAsync(user, request.UserType.ToString());
            if (!roleResult.Succeeded)
                return Result<LoginResponse>.Failure("Failed to assign user role");

            // Generate token
            var authResponse = await _tokenService.GenerateTokenAsync(user);

            // Save refresh token
            var refreshToken = new RefreshToken
            {
                Token = authResponse.RefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<LoginResponse>.Success(authResponse);
        }
    }
}
