using User.Domain.Interfaces.Repositories;
using User.Domain.Interfaces.Sync;
using User.Domain.Models;
using User.Repository.Caching;
using User.Repository.Contexts;
using User.Repository.Repositories;
using User.Repository.SyncComponents;

namespace User.Application.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MongoDbContext>();
            services.AddScoped<IDatabasesSyncRepo<UserModel>, DatabasesSyncRepo<UserModel>>();

            services.AddScoped<IWriteRepository<UserModel>, WriteRepository>();
            services.AddScoped<IReadRepository<UserModel>, ReadRepository<UserModel>>();
            services.EnableCachingDecorator();

            return services;
        }

        private static void EnableCachingDecorator(this IServiceCollection services)
        {
            services.Decorate<IReadRepository<UserModel>, CachingDecorator<UserModel>>();
        }
    }
}