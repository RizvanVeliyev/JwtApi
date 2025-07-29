using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories.Implementations
{
    public class UserRepository:IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<AppUser?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<AppUser?> GetByResetTokenAsync(string resetToken)
        {
            return await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.ResetToken == resetToken);
        }

        public async Task<AppUser?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}
