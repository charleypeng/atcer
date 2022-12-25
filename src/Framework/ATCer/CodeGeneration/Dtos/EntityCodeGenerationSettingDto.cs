// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer.CodeGeneration.Dtos
{
    /// <summary>
    /// 代码生成设置信息
    /// </summary>
    [Description("代码生成设置")]
    public class EntityCodeGenerationSettingDto : BaseDto<int>
    {
        /// <summary>
        /// 实体全称
        /// </summary>
        [Required]
        [DisplayName("实体全称")]
        public string EntityFullName { get; set; }
        /// <summary>
        /// 控制器路由
        /// </summary>
        [DisplayName("控制器路由")]
        public string ControllerRoute { get; set; }
        /// <summary>
        /// 控制器分组   
        /// </summary>
        [DisplayName("控制器分组")]
        public string ControllerGroup { get; set; }
        /// <summary>
        /// 模块名称   
        /// </summary>
        [DisplayName("模块名称")]
        public string ModuleName { get; set; }
    }
}
