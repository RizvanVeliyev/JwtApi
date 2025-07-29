using Application.Services.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Repositories.Abstractions;

namespace Application.Features.Auth.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponseDto>>
    {


        private readonly UserManager<AppUser> _userManager;

        public GetAllUsersQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();
            var userList = new List<UserResponseDto>();

            foreach (var user in users)
            {
                // User-un rollarını çəkirik (UserManager ilə)
                var roles = await _userManager.GetRolesAsync(user);
                var roleName = roles.FirstOrDefault() ?? "Member";

                userList.Add(new UserResponseDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = $"{user.Name} {user.Surname}",
                    Role = Enum.TryParse<UserRole>(roleName, out var parsedRole) ? parsedRole : UserRole.Member
                });
            }

            return userList;
        }
    }
}
