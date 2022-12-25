// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ATCer.Client.Core
{
    /// <summary>
    /// SignalRClientManager
    /// </summary>
    public static class SystemNotificationExtension
    {
        /// <summary>
        /// SignalRClientManager
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSignalRClientManager(this WebAssemblyHostBuilder builder) 
        {
            builder.Services.AddTransient<ISignalRClientBuilder, SignalRClientBuilder>();
            builder.Services.AddScoped<ISignalRClientManager, SignalRClientManager>();
        }

    }
}
