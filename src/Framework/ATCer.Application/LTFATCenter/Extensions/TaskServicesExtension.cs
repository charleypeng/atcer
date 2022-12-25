// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATCer.LTFATCenter.Services;
using Microsoft.Extensions.Hosting;

namespace ATCer.LTFATCenter.Extensions
{
    /// <summary>
    /// 计划服务类
    /// </summary>
    public static class TaskServicesExtension
    {
        /// <summary>
        /// 添加计划服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddLTFATBackgroudService(this IServiceCollection services)
        {
            services.AddSingleton<FipsSyncService>();
            services.AddHostedService(sp => sp.GetRequiredService<FipsSyncService>());
        }
    }
}
