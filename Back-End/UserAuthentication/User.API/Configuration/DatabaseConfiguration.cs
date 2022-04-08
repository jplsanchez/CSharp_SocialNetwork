using Microsoft.EntityFrameworkCore;
using User.Infra;

namespace User.API.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection DatabaseConfigure(this IServiceCollection services, IConfiguration configuration )
        {
            string connString = configuration.GetConnectionString("Postgres");

            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(connString)
                );

            return services;
        }

    }
}
