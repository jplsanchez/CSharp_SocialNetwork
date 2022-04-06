using MediatR;
using User.Domain;
using User.Domain.Interfaces.Repositories;
using User.Domain.Interfaces.Sync;
using User.Domain.Models;
using User.Repository.Caching;
using User.Repository.Contexts;
using User.Repository.Repositories;
using User.Repository.SyncComponents;

namespace User.Application.Configuration
{
    public static class ServicesConfiguration
    {
        private static IConfiguration? _configuration;

        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;

            services.ApplySettings();
            services.AddMemoryCache();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.ResolveDependencies();

            return services;
        }

        private static IServiceCollection ApplySettings(this IServiceCollection services)
        {
            if (_configuration != null)
            {
                Settings settings = new(_configuration);
                services.AddSingleton(settings);

                return services;
            }
            throw new ArgumentNullException(nameof(_configuration));
        }

        private static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MongoDbContext>();
            services.AddScoped<IDatabasesSync<UserModel>, DatabasesSync<UserModel>>();

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