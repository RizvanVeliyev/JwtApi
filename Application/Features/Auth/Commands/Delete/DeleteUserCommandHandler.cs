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


        public DeleteUserCommandHandler(IUserService userService, IHttpContextAccessor contextAccessor, IUserRepository userRepository)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userRole = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole != "SuperAdmin")
                throw new UnauthorizedAccessException("You don't have permission to delete users.");

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                return false;

            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}
