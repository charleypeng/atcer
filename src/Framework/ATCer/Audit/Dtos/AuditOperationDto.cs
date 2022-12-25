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
    /// 操作审计信息
    /// </summary>
    [Description("操作审计信息")]
    public class AuditOperationDto : BaseDto<Guid>
    {
        /// <summary>
        /// 资源名
        /// </summary>
        [DisplayName("资源名")]
        public string ResourceName { get; set; }
        /// <summary>
        /// 资源编号
        /// </summary>
        [DisplayName("资源编号")]
        public Guid ResourceId { get; set; }
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
        /// 访问IP
        /// </summary>
        [DisplayName("IP")]
        public string Ip { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        [DisplayName("UserAgent")]
        public string UserAgent { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        [DisplayName("请求地址")]
        public string Path { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        [DisplayName("请求方法")]
        public MyHttpMethod Method { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        [DisplayName("请求参数")]
        public string Parameters { get; set; }
        /// <summary>
        /// 关联数据审计
        /// </summary>
        public List<AuditEntityDto> AuditEntitys { get; set; }
    }
}
