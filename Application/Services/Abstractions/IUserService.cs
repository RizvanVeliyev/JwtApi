using Application.Features.Auth.Commands.ChangePassword;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.Update;
using Domain.Entities;

namespace Application.Services.Abstractions
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterRequestDto dto);
        Task<User> LoginAsync(LoginRequestDto dto);
        Task<User> FindByEmailAsync(string email);
        Task<bool> IsEmailtakenAsync(string email);
        Task<List<User>> GetAllUsersAsync();
        Task LogoutAsync(string UserId);

        Task<bool> DeleteUserAsync(string userId);
        Task<User> UpdateUserAsync(string userId, UpdateUserRequestDto dto);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequestDto dto);

        Task<string?> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string token, string newPassword, string confirmPassword);



    }
}
