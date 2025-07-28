using Application.Services.Abstractions;
using MediatR;
using Persistence.Repositories.Abstractions;
using Domain.Entities;


namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(ITokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Request;
            var user =await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || user.Password != dto.Password)//hash yoxlanmasi
                throw new Exception("Invalid credentials");

            

            var token = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshTokens ??= new List<RefreshToken>();
            user.RefreshTokens.Add(refreshToken);

            await _userRepository.SaveChangesAsync();

            return new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                Role = user.Role

            };
        }
    }

}
