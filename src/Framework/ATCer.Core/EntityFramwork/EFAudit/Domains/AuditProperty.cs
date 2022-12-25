// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Attributes;
using ATCer.EntityFramwork.DbContexts;
using System;
using System.ComponentModel;

namespace ATCer.EntityFramwork.Audit.Domains
{
    /// <summary>
    /// 实体属性审计信息
    /// </summary>
    [Description("属性审计信息")]
    [IgnoreAudit]
    public class AuditProperty : Entity<Guid, MasterDbContextLocator, ATCerAuditDbContextLocator>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [DisplayName("字段名称")]
        public string FieldName { get; set; }

        /// <summary>
        /// 旧值
        /// </summary>
        [DisplayName("旧值")]
        public string OriginalValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        [DisplayName("新值")]
        public string NewValue { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DisplayName("数据类型")]
        public string DataType { get; set; }

        /// <summary>
        /// 实体审计编号  
        /// </summary>
        [DisplayName("实体审计编号")]
        public Guid AuditEntityid { get; set; }
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
        /// 审计实体
        /// </summary>
        public AuditEntity AuditEntity { get; set; }
    }
}
