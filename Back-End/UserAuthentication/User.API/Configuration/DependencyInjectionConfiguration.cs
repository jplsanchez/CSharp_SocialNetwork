using User.Domain.Interfaces;
using User.Domain.Services;

namespace User.API.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies (this IServiceCollection services)
        {
            services.AddScoped<IRegisterService, RegisterService>();

            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped<ILogoutService, LogoutService>();

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
