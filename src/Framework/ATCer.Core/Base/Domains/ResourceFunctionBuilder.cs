// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using Furion.DatabaseAccessor;

namespace ATCer.Base.Domains
{
    /// <summary>
    /// 资源功能信息配置
    /// </summary>
    public class ResourceFunctionBuilder: IEntityTypeBuilder<ResourceFunction>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<ResourceFunction> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {

            entityBuilder
            .HasKey(t => new { t.ResourceId, t.FunctionId });

            entityBuilder
                .HasOne(pt => pt.Resource)
                .WithMany(p => p.ResourceFunctions)
                .HasForeignKey(pt => pt.ResourceId);

            entityBuilder
                .HasOne(pt => pt.Function)
                .WithMany(t => t.ResourceFunctions)
                .HasForeignKey(pt => pt.FunctionId);
        }
    }
}
