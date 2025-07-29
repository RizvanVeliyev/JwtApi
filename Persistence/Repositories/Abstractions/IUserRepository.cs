using Domain.Entities;

namespace Persistence.Repositories.Abstractions
{
    public interface IUserRepository
    {

        Task<AppUser?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<int> SaveChangesAsync();

        Task<AppUser?> GetByResetTokenAsync(string resetToken);
        Task<AppUser?> GetByRefreshTokenAsync(string refreshToken);



    }
}
