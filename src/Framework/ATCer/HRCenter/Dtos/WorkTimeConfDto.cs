// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer.HRCenter.Dtos
{
    /// <summary>
    /// 工作时段配置Dto
    /// IsDeleted = false为唯一项
    /// </summary>
    public class WorkTimeConfDto: ATCerBaseDto<int>
    {
        /// <summary>
        /// 白班
        /// </summary>
        [Description("白班")]
        public TimeSpan DaySpan { get; set; } = new TimeSpan(8, 0, 0);
        /// <summary>
        /// 夜班
        /// </summary>
        [Description("夜班")]
        public TimeSpan NightSpan { get; set; } = new TimeSpan(22, 0, 0);
        /// <summary>
        /// 版本
        /// </summary>
        [Description("版本")]
        public int Version { get; set; }
        /// <summary>
        /// 变更原因
        /// </summary>
        [Description("变更原因")]
        [MaxLength(100)]
        public string ChangeLog { get; set; }
    }
}
