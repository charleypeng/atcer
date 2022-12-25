// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Audit.Core;
using ATCer.EntityFramwork.Audit.Core;
using ATCer.EntityFramwork.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 审计
    /// </summary>
    public static class AuditExtensions
    {
        /// <summary>
        /// 添加审计服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAudit(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                //审计过滤器
                options.Filters.Add<AuditActionFilter>();
            });
            //数据管理
            services.AddScoped<IOrmAuditService, EntityFramworkAuditService<ATCerAuditDbContextLocator>>();
            return services;
        }
    }
}
