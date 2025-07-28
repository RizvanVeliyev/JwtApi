using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.Abstractions;
using Persistence.Repositories.Implementations;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services) 
        {
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


            return services;

        }
    }
}
