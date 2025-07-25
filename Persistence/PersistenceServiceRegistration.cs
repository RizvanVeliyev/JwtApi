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
            return services;

        }
    }
}
