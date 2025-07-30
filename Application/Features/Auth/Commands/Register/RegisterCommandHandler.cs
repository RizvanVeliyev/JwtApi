using Application.Services.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;


        public RegisterCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }


        public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Request;

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("User already exists");

            var names = dto.FullName?.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var name = names != null && names.Length > 0 ? names[0] : "";
            var surname = names != null && names.Length > 1 ? names[1] : "";


            var user = new AppUser
            {
                UserName = dto.FullName,  
                Email = dto.Email,
                Name = name,
                Surname = surname,
                TwoFactorEnabled = true
            };
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshExpires = DateTime.UtcNow.AddDays(7);

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                //hazir goturmusem
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            

            var roleName = dto.Role.ToString();
            await _userManager.AddToRoleAsync(user, roleName);

          
            return new RegisterResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = dto.FullName,
                TwoFactorEnabled = user.TwoFactorEnabled
                //automapper yazacam
            };

        }
    }
}
