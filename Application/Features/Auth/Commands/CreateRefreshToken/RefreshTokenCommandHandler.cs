using Application.Services.Abstractions;
using MediatR;
using Persistence.Repositories.Abstractions;
using System.Linq;
using System.Security.Authentication;

namespace Application.Features.Auth.Commands.CreateRefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);
            if (user == null)
                throw new AuthenticationException("Invalid refresh token");

            // İndi user.RefreshToken tək string olduğu üçün:
            if (user.RefreshToken != request.RefreshToken || user.RefreshRevoked != null || user.RefreshExpires <= DateTime.UtcNow)
                throw new AuthenticationException("Refresh token is expired or revoked");

            // Refresh tokeni yeniləyirik
            user.RefreshRevoked = DateTime.UtcNow;

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshExpires = DateTime.UtcNow.AddDays(7); 
            user.RefreshRevoked = null; 

            await _userRepository.SaveChangesAsync();

            var accessToken = await _tokenService.CreateToken(user);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
        }
    }

}
