//// -----------------------------------------------------------------------------
//// ATCer 全平台综合性空中交通管理系统
////  作者：彭磊  
////  CopyRight(C) 2022  版权所有 
//// -----------------------------------------------------------------------------

//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using SqlSugar;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Furion;
//using Furion.DatabaseAccessor;
//using Microsoft.Extensions.Hosting;

//namespace ATCer.Base.Extentions
//{
//    public static class SqlsugarSetup
//    {
//        private static string dbProvider = App.Configuration["DefaultDbSettings:DbProvider"];
//        /// <summary>
//        /// 添加SqlSugar
//        /// </summary>
//        /// <param name="services"></param>
//        /// <param name="dbName"></param>
//        public static void AddSqlsugarSetup(this IServiceCollection services, string dbName = "Default")
//        {
//            var dbProviderStrs = dbProvider.Split('.');
//            var provider = dbProviderStrs.LastOrDefault();
//            //如果多个数数据库传 List<ConnectionConfig>
//            var configConnection = new ConnectionConfig()
//            {
//                DbType =(DbType)Enum.Parse(typeof(DbType), provider, ignoreCase:false),
//                ConnectionString = App.Configuration.GetConnectionString(dbName),
//                IsAutoCloseConnection = true,
//            };

//            SqlSugarScope sqlSugar = new SqlSugarScope(configConnection,
//                db =>
//                {
//                    //单例参数配置，所有上下文生效
//                    db.Aop.OnLogExecuting = (sql, pars) =>
//                    {
//                        if(App.HostEnvironment.IsDevelopment())
//                            Console.WriteLine(sql);//输出sql
//                    };
//                });
//            services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
//        }
//    }
//}
