using Application.Features.Auth.Commands.Login;
using Application.Services.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.VerifyTwoFactor
{
    public class VerifyTwoFactorCommandHandler : IRequestHandler<VerifyTwoFactorCommand, LoginResponseDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public VerifyTwoFactorCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> Handle(VerifyTwoFactorCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Request;

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return new LoginResponseDto { Message = "User not found." };


            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", dto.TwoFactorCode);

            if (!isValid)
                return new LoginResponseDto { Message = "Invalid two-factor code." };

            var jwtToken = await _tokenService.CreateToken(user);

            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshExpires = DateTime.UtcNow.AddDays(7);
            user.RefreshRevoked = null;

            await _userManager.UpdateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            var roleEnum = Enum.Parse<UserRole>(roles.FirstOrDefault() ?? UserRole.Member.ToString());

            return new LoginResponseDto
            {
                RequiresTwoFactor = false, 
                Token = jwtToken,
                RefreshToken = refreshToken,
                Role = roleEnum,
                Message = "Login successful."
            };


        }
    }
}
