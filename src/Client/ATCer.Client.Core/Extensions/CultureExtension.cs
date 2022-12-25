// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.Client.Base.Constants;
using ATCer.Client.Core.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Threading.Tasks;

namespace ATCer.Client.Core
{
    public static class CultureExtension
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public async static Task<WebAssemblyHost> UseCulture(this WebAssemblyHost host)
        {
            var jsTool = host.Services.GetRequiredService<IJsTool>();
            var result = await jsTool.SessionStorage.GetAsync<string>(ClientConstant.BlazorCultureKey);
            var culture = new CultureInfo(string.IsNullOrEmpty(result) ? "zh-CN" : result);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            //
            LocalizerUtil.Localizer = host.Services.GetRequiredService<IClientLocalizer>();
            return host;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public static IServiceCollection AddCulture<T>(this IServiceCollection services)
        {
            services.AddScoped<IClientLocalizer, ClientLocalizer<T>>();
            return services;
        }
    }
}
