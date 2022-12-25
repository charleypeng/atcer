// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Furion.DatabaseAccessor;
using ATCer.Base.Enums;

namespace ATCer.Base.Domains
{
    /// <summary>
    /// 资源表配置
    /// </summary>
    public class ResourceBuilder: IEntityTypeBuilder<Resource>, IEntitySeedData<Resource>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Resource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
               .HasMany(x => x.Children)
               .WithOne(x => x.Parent)
               .HasForeignKey(x => x.ParentId)
               .OnDelete(DeleteBehavior.ClientSetNull); // 必须设置这一行
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {
                new Resource() {
                    Name="后台根节点",Key="admin_root",Remark="根根节点不能删除，不能改变类型！！。",Path="",Icon="apartment",Order=0,Type=Enum.Parse<ResourceType>("Root"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2022-08-16 07:13:50"),Id=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868")},
                new Resource() {
                    Name="前台根节点",Key="front_root",Remark="根根节点不能删除，不能改变类型！！。",Path="",Icon="apartment",Order=1,Type=Enum.Parse<ResourceType>("Root"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2022-08-16 07:15:50"),Id=Guid.Parse("f4239a53-b5e1-49bd-99c6-967a86f07cdc")},
            };
        }
    }
}
