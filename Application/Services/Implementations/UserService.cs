using Application.Dtos;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Services.Abstractions;
using Domain.Entities;

namespace Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private static readonly List<User> _users = new List<User>();
        public Task<User> FindByEmailAsync(string email)
        {
            var user=_users.FirstOrDefault(x=>x.Email == email);
            return Task.FromResult(user);   
        }

        public Task<bool> IsEmailtakenAsync(string email)
        {
            return Task.FromResult(_users.Any(x => x.Email == email));
        }

        public Task<User> LoginAsync(LoginRequestDto dto)
        {
            var user= _users.FirstOrDefault(x=>x.Email == dto.Email && x.Password==dto.Password);
            if (user == null) throw new Exception("Invalid credentials");
            return Task.FromResult(user);

        }

        public Task<User> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> RegisterAsync(RegisterRequestDto dto)
        {
            if(_users.Any(x=>x.Email== dto.Email))
                throw new Exception("Email already exists");

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = dto.Email,
                Password = dto.Password,
                FullName = dto.FullName
            };

            _users.Add(user);
            return Task.FromResult(user);
        }
    }
}
