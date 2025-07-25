using Application.Services.Abstractions;
using MediatR;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Auth.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponseDto>>
    {
        

        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                Role=u.Role
            }).ToList();
        }
    }
}
