using MediatR;

namespace Application.Features.Auth.Commands.Logout
{
    public class LogoutCommand:IRequest<Unit>
    {
        public string UserId { get; set; }
    }
}
