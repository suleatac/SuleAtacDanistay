using App.Bus;
using App.UI.Consumer;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.UI.Extentions
{
    public static class BusExtentions
    {
        public static IServiceCollection AddBusExtentions(this IServiceCollection services,IConfiguration configuration) 
        {

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<ConsumerDocument>();

                var busConnectionToString = configuration.GetSection(BusOption.Key).Get<BusOption>();
                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busConnectionToString!.Adress}:{busConnectionToString.Port}"), h =>
                    {
                        h.Username(busConnectionToString.UserName);
                        h.Password(busConnectionToString.Password);
                    });

                    cfg.ReceiveEndpoint("App-UI.publish-document-command.queue", e =>
                    {
                        e.ConfigureConsumer<ConsumerDocument>(context);

                    });
                });
            });

            return services;
        }
    }
}
