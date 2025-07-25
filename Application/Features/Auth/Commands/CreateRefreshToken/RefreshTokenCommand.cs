using MediatR;

namespace Application.Features.Auth.Commands.CreateRefreshToken
{
    public class RefreshTokenCommand : IRequest<AuthResponseDto>
    {
        public string RefreshToken { get; set; } = null!;
    }
}
