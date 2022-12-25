// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Enums;
using ATCer.Base;
using ATCer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ATCer.Audit.Dtos
{
    /// <summary>
    /// 实体审计信息
    /// </summary>
    [Description("实体审计信息")]
    public class AuditEntityDto : BaseDto<Guid>
    {
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
        /// 获取或设置 操作实体属性集合
        /// </summary>
        public ICollection<AuditPropertyDto> AuditProperties { get; set; }
    }
}
