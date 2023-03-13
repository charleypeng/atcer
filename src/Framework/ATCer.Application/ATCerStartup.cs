// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter;
using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Enums;
using ATCer.MessageCenter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Newtonsoft.Json;


namespace ATCer.Core
{
    [AppStartup(800)]
    public class ATCerStartup:AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataCenterService();
        }
    }
}
