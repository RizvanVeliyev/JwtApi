using MediatR;

namespace Application.Features.Auth.Commands.Delete
{
    public class DeleteUserCommand:IRequest<bool>
    {
        public string UserId { get; set; }
    }
}
