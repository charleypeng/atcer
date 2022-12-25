// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer;
using ATCer.Client.Services;
using Plk.Blazor.DragDrop;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 添加ATCer服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddATCerComponents(this IServiceCollection services)
        {
            services.AddBlazorDragDrop();
            services.AddSingleton<UserOptions>();
            services.AddScoped<IThemeService, ThemeService>();
        }
    }
}
