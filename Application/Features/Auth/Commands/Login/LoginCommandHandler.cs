using Application.Dtos;
using Application.Services.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Repositories.Abstractions;


namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public LoginCommandHandler(ITokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Request;

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                throw new Exception("Invalid credentials");

            if (await _userManager.GetTwoFactorEnabledAsync(user))
            {
                var twoFactorToken = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                await _emailService.SendEmailAsync(new EmailSendDto
                {
                    ToEmail = user.Email,
                    Subject = "Your 2FA Code",
                    Body = $"Your two-factor authentication code is: {twoFactorToken}"
                });

                return new LoginResponseDto
                {
                    RequiresTwoFactor = true,
                    Message = "Two-factor authentication code sent to your email."
                };
            }




            var token = await _tokenService.CreateToken(user);
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
                Token = token,
                RefreshToken = refreshToken,
                Role = roleEnum,
                Message = "Login successful."


            };
        }
    }

}
