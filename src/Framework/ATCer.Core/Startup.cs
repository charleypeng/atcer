// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.EntityFramwork.DbContexts;
using ATCer.UserCenter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using ATCer.SysJob.Core;

#nullable disable
namespace ATCer
{
    /// <summary>
    /// Dbcontext默认启动项
    /// </summary>
    [AppStartup(500)]
    public class Startup : AppStartup
    {
        /// <summary>
        /// 迁移程序集名称
        /// </summary>
        private static readonly string migrationAssemblyName = App.Configuration["DefaultDbSettings:MigrationAssemblyName"];
        /// <summary>
        /// 数据库提供器
        /// </summary>
        private static readonly string dbProvider = App.Configuration["DefaultDbSettings:DbProvider"];
        /// <summary>
        /// 初始化默认数据库
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            if (dbProvider == DbProvider.Npgsql)
            {
                //解决切换postgresql时可能出错 
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                //AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            }
            services.AddDatabaseAccessor(options =>
            {
                //注入数据库上下文
                options.AddDbPool<ATCerDbContext>(dbProvider);
                options.AddDbPool<ATCerAuditDbContext, ATCerAuditDbContextLocator>(dbProvider);
            }, migrationAssemblyName);

            //分布式系统作业
            //services.AddSchedule(options =>
            //{
            //    options.AddPersistence<DbJobPersistence>(); // 添加作业持久化器
            //});添加作业持久化器
        }

        /// <summary>
        /// 执行数据库初始化和迁移
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var logger = App.GetService<ILogger<Startup>>();
            var initDb = bool.Parse(App.Configuration["DefaultDbSettings:InitDb"]);
            var autoMigration = bool.Parse(App.Configuration["DefaultDbSettings:AutoMigration"]);
            // 判断开发环境！！！必须！！！！
            if (env.IsDevelopment())
            {
                Scoped.Create((_, scope) =>
                {
                    var defaultDbContext = scope.ServiceProvider.GetRequiredService<ATCerDbContext>();
                    var auditDbContext = scope.ServiceProvider.GetRequiredService<ATCerAuditDbContext>();
                    if (initDb)
                    {
                        defaultDbContext.Database.EnsureCreated();
                        auditDbContext.Database.EnsureCreated();
                        logger.LogInformation("数据库初始化完成");
                    }
                    if (autoMigration)
                    {
                        defaultDbContext.Database.Migrate();
                        auditDbContext.Database.Migrate();
                        logger.LogInformation("数据库迁移完成");
                    }
                });
            }

            //start up usercenter
            var service = App.GetService<IAppTokenServce>();
            await service.ImportToCache();
        }
    }
}
