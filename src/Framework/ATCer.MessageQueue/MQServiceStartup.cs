using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ATCer.MessageQueue.Core;
using ATCer.MessageCenter.Services;

namespace ATCer.MessageQueue
{
    /// <summary>
    /// Message queue start up
    /// </summary>
    [AppStartup(500)]
    public class MessageQueueStartup:AppStartup
    {
        /// <summary>
        /// Config service
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //add MessageQueue options
            services.AddConfigurableOptions<MessageQueueOptions>();
            //add MQService
            services.AddMQService();
            //add signalr
            services.TryAddSingleton<IMQService, MQService>();
        }

        /// <summary>
        /// Config app
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //use dashboard
        
            //clear all status on first init
            var userStatusService = App.GetService<IUserStatusNotifyService>();
            userStatusService.ClearAllStatus();
        }
    }
}