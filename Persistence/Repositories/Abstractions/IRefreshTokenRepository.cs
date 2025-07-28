using Domain.Entities;

namespace Persistence.Repositories.Abstractions
{
    public interface IRefreshTokenRepository
    {
        Task<List<RefreshToken>> GetByUserIdAsync(string userId);
        Task DeleteRangeAsync(IEnumerable<RefreshToken> tokens);
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task SaveChangesAsync();
    }
}
