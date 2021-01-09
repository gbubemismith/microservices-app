using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ordering.API.RabbitMQ;

namespace Ordering.API.Helpers
{
    public static class Extensions
    {
        public static EventBusRabbitMqConsumer Listener { get; set; }
        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusRabbitMqConsumer>();

            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);



            return app;
        }

        private static void OnStarted()
        {
            Listener.Consume();
        }

        private static void OnStopping()
        {
            Listener.Disconnect();
        }
    }
}