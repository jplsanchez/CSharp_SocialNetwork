using MassTransit;
using User.Application.QueueHandlers;

namespace User.Application.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void MassTransitConfigure(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserConsumer>();
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    config.ReceiveEndpoint("user-queue", cfg =>
                    {
                        cfg.ConfigureConsumer<UserConsumer>(context);
                    });
                });
            });
        }
    }
}
