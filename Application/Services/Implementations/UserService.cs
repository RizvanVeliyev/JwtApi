using Application.Features.Auth.Commands.ChangePassword;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.Update;
using Application.Services.Abstractions;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private static readonly List<User> _users = new List<User>();
        public Task<User> FindByEmailAsync(string email)
        {
            var user = _users.FirstOrDefault(x => x.Email == email);
            return Task.FromResult(user);
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return Task.FromResult(_users);
        }


        public Task<bool> IsEmailtakenAsync(string email)
        {
            return Task.FromResult(_users.Any(x => x.Email == email));
        }

        public Task<User> LoginAsync(LoginRequestDto dto)
        {
            var user = _users.FirstOrDefault(x => x.Email == dto.Email && x.Password == dto.Password);
            if (user == null) throw new Exception("Invalid credentials");
            return Task.FromResult(user);

        }



        public async Task LogoutAsync(string UserId)
        {
            var user = _users.FirstOrDefault(u => u.Id == UserId);
            //if (user != null)
            //{
            //    // Bütün refresh tokenləri silirik
            //    user.RefreshTokens.Clear();
            //}

            await Task.CompletedTask;
        }

        public Task<User> RegisterAsync(RegisterRequestDto dto)
        {
            if (_users.Any(x => x.Email == dto.Email))
                throw new Exception("Email already exists");

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = dto.Email,
                Password = dto.Password,
                FullName = dto.FullName,
                Role = dto.Role
            };

            _users.Add(user);
            return Task.FromResult(user);
        }




        public Task<bool> DeleteUserAsync(string userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
                return Task.FromResult(false);

            _users.Remove(user);
            return Task.FromResult(true);
        }

        public Task<User> UpdateUserAsync(string userId, UpdateUserRequestDto dto)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new Exception("User not found");

            user.FullName = dto.FullName;
            return Task.FromResult(user);
        }


        public Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequestDto dto)
        {
            var user = _users.FirstOrDefault(x => x.Id == userId);
            if (user == null) throw new Exception("User not found");

            if (user.Password != dto.CurrentPassword)
                throw new Exception("Current password is incorrect");

            if (dto.NewPassword != dto.ConfirmPassword)
                throw new Exception("New passwords do not match");

            user.Password = dto.NewPassword;
            return Task.FromResult(true);
        }

        public Task<string?> ForgotPasswordAsync(string email)
        {
            var user=_users.FirstOrDefault(X=>X.Email == email);
            if (user == null)
                return Task.FromResult<string?>(null);

            var token=Guid.NewGuid().ToString();
            user.ResetToken = token;
            user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(15);

            var link = $"http://localhost:5000/api/auth/reset-password?token={token}";
            return Task.FromResult<string?>(link);

        }

        public Task<bool> ResetPasswordAsync(string token, string newPassword, string confirmPassword)
        {
            var user = _users.FirstOrDefault(u =>
                u.ResetToken == token &&
                u.ResetTokenExpiry > DateTime.UtcNow);

            if (user == null)
                return Task.FromResult(false);

            if (newPassword != confirmPassword)
                return Task.FromResult(false);

            user.Password = newPassword;
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            return Task.FromResult(true);
        }

    }
}
