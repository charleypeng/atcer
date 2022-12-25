// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer.SystemManager.Dtos
{
    /// <summary>
    /// 资源功能关系信息
    /// </summary>
    [Description("资源功能关系信息")]
    public class ResourceFunctionDto
    {

        /// <summary>
        /// 权限Id
        /// </summary>
        [Required(ErrorMessage = "不能为空")]
        [DisplayName("资源编号")]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 功能Id
        /// </summary>
        [Required(ErrorMessage = "不能为空")]
        [DisplayName("功能编号")]
        public Guid FunctionId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
    }
}
