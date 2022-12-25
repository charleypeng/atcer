// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using ATCer.EventBus;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class EventBusServiceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptionsBuilder"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBusServices(this IServiceCollection services, Action<EventBusOptionsBuilder> configureOptionsBuilder)
        {
            services.AddScoped<IEventBus, EventBusService>();
            services.AddEventBus(configureOptionsBuilder);
            return services;
        }
    }
}
