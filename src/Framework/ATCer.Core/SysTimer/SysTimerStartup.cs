// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion;
using ATCer.SysTimer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace ATCer.SysTimer.Impl
{
    /// <summary>
    /// 任务调度启动项
    /// </summary>
    [AppStartup(500)]
    public class SysTimerStartup : AppStartup
    {
        /// <summary>
        /// 启动调度任务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
        }
    }
}
