using ATCer.WiseATC.Options;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace ATCer.WiseATC
{
    [AppStartup(0)]
    public class Startup:AppStartup
    {
        /// <summary>
        /// Config service
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //add MessageQueue options
            services.AddConfigurableOptions<RabitMQOptions>();
            //add MQService
            var config = App.Configuration;

            string assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //get options
            using var serviceProvider = services.BuildServiceProvider();
            var rmq = serviceProvider.GetService<IOptions<RabitMQOptions>>().Value;

            if(rmq == null)
                throw new ArgumentNullException("rabbit options is null");

            var conStr = $"amqp://{rmq.UserName}:{rmq.Password}@{rmq.HostName}:{rmq.Port}/";
            services.AddRabbitMQEventBus(() => conStr, eventBusOptionAction: option =>
            {
                option.ClientProvidedAssembly(assemblyName);
                option.EnableRetryOnFailure(true, 5000, TimeSpan.FromSeconds(5));
                option.LoggingWriteLevel(LogLevel.Warning);
                option.MessageTTL(2000);
                option.DeadLetterExchangeConfig(cfg =>
                {
                    cfg.Enabled = true;
                    cfg.ExchangeNameSuffix = "-atcer-ext";
                });
            });

            //services.AddButterfly
        }

        /// <summary>
        /// Config app
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.RabbitMQEventBusAutoSubscribe();
        }
    }
}