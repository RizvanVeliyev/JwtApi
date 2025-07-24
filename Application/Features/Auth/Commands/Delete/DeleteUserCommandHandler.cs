using Application.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Application.Features.Auth.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public DeleteUserCommandHandler(IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userRole = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole != "SuperAdmin")
                throw new UnauthorizedAccessException("You don't have permission to delete users.");

            return await _userService.DeleteUserAsync(request.UserId);
        }
    }
}
