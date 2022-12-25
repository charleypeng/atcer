// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Attributes;
using ATCer.Authentication.Enums;
using ATCer.EntityFramwork.DbContexts;
using ATCer.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATCer.EntityFramwork.Audit.Domains
{
    /// <summary>
    /// 审计实体表
    /// </summary>
    [Description("实体审计信息")]
    [IgnoreAudit]
    public class AuditEntity : Entity<Guid, MasterDbContextLocator, ATCerAuditDbContextLocator>
    {
        /// <summary>
        /// 审计实体表
        /// </summary>
        public AuditEntity() 
        {
            this.AuditProperties = new List<AuditProperty>();
        }
        /// <summary>
        /// 数据编号
        /// </summary>
        [DisplayName("数据编号")]
        public string DataId { get; set; }
        /// <summary>
        /// 实体名称
        /// </summary>
        [DisplayName("实体名称")]
        public string Name { get; set; }
        /// <summary>
        /// 实体类型名称
        /// </summary>
        [DisplayName("实体类型名称")]
        public string TypeName { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        [DisplayName("操作类型")]
        public EntityOperationType OperationType { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        [DisplayName("操作者编号")]
        public string OperaterId { get; set; }
        /// <summary>
        /// 操作者名称
        /// </summary>
        [DisplayName("操作者名称")]
        public string OperaterName { get; set; }
        /// <summary>
        /// 操作者类型
        /// </summary>
        [DisplayName("操作者类型")]
        public IdentityType OperaterType { get; set; }
        /// <summary>
        /// 操作ID
        /// </summary>
        [DisplayName("操作审计编号")]
        public Guid OperationId { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 操作实体属性集合
        /// </summary>
        public ICollection<AuditProperty> AuditProperties { get; set; }
        /// <summary>
        /// 新值
        /// </summary>
        [NotMapped]
        public PropertyValues CurrentValues { get; set; }
        /// <summary>
        /// 老值
        /// </summary>
        [NotMapped]
        public PropertyValues OldValues { get; set; }
    }
}
