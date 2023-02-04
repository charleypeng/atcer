// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace ATCer.HRCenter.Domains
{
    /// <summary>
    /// 工作时段配置
    /// IsDeleted = false为唯一项
    /// </summary>
    [Comment("工作时段配置")]
    public class WorkTimeConf:ATCerEntityBase, IBaseEntity
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
        /// 系数
        /// </summary>
        [Description("系数")]
        public double NightShiftMultiplier { get; set; }
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
