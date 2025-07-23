using Application.Services.Abstractions;
using MediatR;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Request;
            var user = await _userService.LoginAsync(dto);

            var token = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }

}
