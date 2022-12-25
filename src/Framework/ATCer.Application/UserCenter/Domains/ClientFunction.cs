// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Base.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace ATCer.UserCenter.Impl.Domains
{
    /// <summary>
    /// 客户端功能信息
    /// </summary>
    [Description("客户端功能信息")]
    public class ClientFunction : IEntity, IEntityTypeBuilder<ClientFunction>
    {
        /// <summary>
        /// 客户端编号
        /// </summary>
        [Required]
        [DisplayName("客户端编号")]
        public Guid ClientId { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        [DisplayName("客户端")]
        public Client Client { get; set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        [Required]
        [DisplayName("功能编号")]
        public Guid FunctionId { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        [DisplayName("功能")]
        public Function Function { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<ClientFunction> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasKey(t => new { t.ClientId, t.FunctionId });

            entityBuilder
                .HasOne(pt => pt.Client)
                .WithMany(t => t.ClientFunctions)
                .HasForeignKey(pt => pt.ClientId);
        }
    }
}
