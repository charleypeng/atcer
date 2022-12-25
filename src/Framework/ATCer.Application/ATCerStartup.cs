// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.MessageCenter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace ATCer.Core
{
    [AppStartup(501)]
    public class ATCerStartup:AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}
