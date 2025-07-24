using MediatR;

namespace Application.Features.Auth.Queries.GetAllUsers
{
    public class GetAllUsersQuery:IRequest<List<UserResponseDto>>
    {
    }
}
