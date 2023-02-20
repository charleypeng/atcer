// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace ATCer.UserCenter.Impl.Domains
{
    /// <summary>
    /// 部门信息
    /// </summary>
    [Description("部门信息")]
    public class Dept: ATCerEntityBase, IEntityTypeBuilder<Dept>, IEntitySeedData<Dept>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required, MaxLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        [MaxLength(20)]
        public string Contacts { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        [MaxLength(20)]
        public string Tel { get; set; }

        /// <summary>
        /// 资源排序
        /// </summary>
        [Required, DefaultValue(0)]
        [DisplayName("排序")]
        public int Order { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [DisplayName("父级编号")]
        public int? ParentId { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public Dept Parent { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<Dept> Children { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<User> Users { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Dept> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
              .HasMany(x => x.Children)
              .WithOne(x => x.Parent)
              .HasForeignKey(x => x.ParentId)
              .OnDelete(DeleteBehavior.ClientSetNull); // 必须设置这一行
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Dept> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new Dept[]
            {
                new Dept{ Id=1,Name="湖南空管分局",Contacts="test1",Tel="400-8888888",Order=1,Remark="湖南空管分局",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
                new Dept{ Id=2,ParentId=1, Name="塔台",Contacts="test1",Tel="400-8888888",Order=1,Remark="湖南空管分局",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
                new Dept{ Id=3,ParentId=1,Name="进近",Contacts="test1",Tel="400-8888888",Order=1,Remark="湖南空管分局",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
                new Dept{ Id=4,ParentId=1,Name="区调",Contacts="test1",Tel="400-8888888",Order=1,Remark="湖南空管分局",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
                new Dept{ Id=5,Name="机场公司",Contacts="test1",Tel="400-8888888",Order=1,Remark="机场公司",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
                new Dept{ Id=6,ParentId=5,Name="机坪",Contacts="test1",Tel="400-8888888",Order=1,Remark="机场公司",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
            };
        }
    }
}
