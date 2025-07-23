using Application.Dtos;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Domain.Entities;

namespace Application.Services.Abstractions
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterRequestDto dto);
        Task<User> LoginAsync(LoginRequestDto dto);
        Task<User> LogoutAsync();
        Task<User> FindByEmailAsync(string email);
        Task<bool> IsEmailtakenAsync(string email);

    }
}
