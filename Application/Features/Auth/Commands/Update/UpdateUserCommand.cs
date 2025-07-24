using MediatR;

namespace Application.Features.Auth.Commands.Update
{
    public class UpdateUserCommand : IRequest<UpdateUserResponseDto>
    {
        public UpdateUserRequestDto Request { get; set; }

        
    }
}
