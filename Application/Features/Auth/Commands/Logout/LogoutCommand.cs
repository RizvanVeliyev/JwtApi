using MediatR;

namespace Application.Features.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest<bool>
    {
        public string RefreshToken { get; set; } = null!;
    }
}
