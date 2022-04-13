using MediatR;
using User.Domain;

namespace User.Application.Configuration
{
    public static class ServicesConfiguration
    {
        private static Settings? _settings;

        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            _settings = new Settings(configuration);

            services.ApplySettings();

            services.AddMemoryCache();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddLogging();

            services.AutoMapperConfigure();

            services.MassTransitConfigure();
            services.ResolveDependencies();

            services.AuthenticationConfigure(_settings);

            return services;
        }

        private static IServiceCollection ApplySettings(this IServiceCollection services)
        {
            if (_settings != null)
            {
                services.AddSingleton(_settings);

                return services;
            }
            throw new ArgumentNullException(nameof(_settings));
        }
    }
}