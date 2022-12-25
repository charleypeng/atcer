﻿// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;

namespace ATCer.LTFATCenter.DbContexts
{
    /// <summary>
    /// 从数据库
    /// </summary>
    [AppDbContext("SugarTest")]
    public class ATCerSlaveDbContext: AppDbContext<ATCerSlaveDbContext, ATCerSlaveDbContextLocator>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="options"></param>
        public ATCerSlaveDbContext(DbContextOptions<ATCerSlaveDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var cstr = App.Configuration["ConnectionStrings:SugarTest"];
            optionsBuilder.UseNpgsql(cstr).UseLowerCaseNamingConvention();
            // base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// 解决sqlite 不支持datetimeoffset问题（损失很小的精度）
        /// https://blog.dangl.me/archive/handling-datetimeoffset-in-sqlite-with-entity-framework-core/
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                // SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
                // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
                // To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
                // use the DateTimeOffsetToBinaryConverter
                // Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
                // This only supports millisecond precision, but should be sufficient for most use cases.
                foreach (var entityType in builder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset)
                                                                                || p.PropertyType == typeof(DateTimeOffset?));
                    foreach (var property in properties)
                    {
                        builder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}
