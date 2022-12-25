// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Common.JsonConverters;
using ATCer.NotificationSystem.Core;
using ATCer.NotificationSystem.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ATCer.NotificationSystem
{
    /// <summary>
    /// 系统通知服务扩展
    /// </summary>
    public static class SystemNotificationExtensions
    {

        /// <summary>
        /// 添加系统通知服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSystemNotify(this IServiceCollection services)
        {
            
            //添加配置信息
            services.AddConfigurableOptions<SignalROptions>();
            // 添加即时通讯
            services.AddSignalR().AddJsonProtocol(options => {
                options.PayloadSerializerOptions = new System.Text.Json.JsonSerializerOptions() 
                {
                    WriteIndented = true,
                    Converters =
                    {
                        new DateTimeJsonConverter(),
                        new DateTimeOffsetJsonConverter(),
                        new NotificationDataJsonConverter()
                    }
                };
            }); ;
            services.TryAddSingleton<IUserIdProvider, JwtUserIdProvider>();
            services.TryAddSingleton<ISystemNotificationService, SystemNotificationService>();
            return services;
        }
    }
}
