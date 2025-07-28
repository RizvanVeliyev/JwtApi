using Application.Services.Abstractions;
using MediatR;
using Persistence.Repositories.Abstractions;
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
            var user = await _userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
            if (user == null)
                throw new AuthenticationException("Invalid refresh token");

            var refreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == request.RefreshToken);

            if (refreshToken == null || refreshToken.Revoked != null || refreshToken.Expires <= DateTime.UtcNow)
                throw new AuthenticationException("Refresh token is expired or revoked");

            refreshToken.Revoked = DateTime.UtcNow;

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);

            await _userRepository.SaveChangesAsync();

            var accessToken = _tokenService.CreateToken(user);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token
            };
        }
    }
}
