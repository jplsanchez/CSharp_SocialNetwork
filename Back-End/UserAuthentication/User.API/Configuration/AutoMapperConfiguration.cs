namespace User.API.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AutoMapperConfigure(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

    }
}
