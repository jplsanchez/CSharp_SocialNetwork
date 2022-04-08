using Microsoft.AspNetCore.Identity;
using User.Infra;

namespace User.API.Configuration
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection IdentityConfigure(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<UserDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
            });

            return services;
        }
    }
}