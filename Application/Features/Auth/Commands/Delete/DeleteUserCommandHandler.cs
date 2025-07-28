using Application.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Repositories.Abstractions;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Application.Features.Auth.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;


        public DeleteUserCommandHandler(IUserService userService, IHttpContextAccessor contextAccessor, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //var userRole = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            //if (userRole != "SuperAdmin")
            //    throw new UnauthorizedAccessException("You don't have permission to delete users.");

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                return false;


            var refreshTokens = await _refreshTokenRepository.GetByUserIdAsync(user.Id);
            if (refreshTokens.Any())
            {
                await _refreshTokenRepository.DeleteRangeAsync(refreshTokens);
                await _refreshTokenRepository.SaveChangesAsync();
            }

            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}
