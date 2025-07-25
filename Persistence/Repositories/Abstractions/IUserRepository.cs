using Domain.Entities;

namespace Persistence.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> AnyByEmailAsync(string email);
        Task<User?> GetByIdAsync(string userId);
        Task SaveChangesAsync();
        Task DeleteAsync(User user);
        Task<User?> GetByResetTokenAsync(string token);
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<List<User>> GetAllAsync();



    }
}
