// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

#nullable disable
namespace ATCer.HRCenter.Domains
{
    /// <summary>
    /// 执勤小时计算信息
    /// </summary>
    [Comment("执勤小时计算信息")]
    public class CalTimeItem: IEntity<MasterDbContextLocator>, IBaseEntity
    {
        public long Id { get; set; }
        /// <summary>
        /// 管制员Id
        /// </summary>
        [Description("Id")]
        public int UserId { get; set; }
        /// <summary>
        /// 执勤时间信息
        /// </summary>
        [DisplayName("执勤时间Id")]
        public long TimeItemId { get; set; }
        /// <summary>
        /// 白班时间
        /// </summary>
        [DisplayName("白班时间")]
        public TimeSpan DayTimeSpan { get; set; }
        /// <summary>
        ///夜班时间
        /// </summary>
        [DisplayName("夜班时间")]
        public TimeSpan NightTimeSpan { get; set; }
        /// <summary>
        ///扇区系数
        /// </summary>
        [DisplayName("扇区系数")]
        public double SectorMultiplier { get; set; }
        /// <summary>
        ///岗位系数
        /// </summary>
        [DisplayName("岗位系数")]
        public double PositionMultiplier { get; set; }
        /// <summary>
        ///夜班系数
        /// </summary>
        [DisplayName("夜班系数")]
        public double ATCMultiplier { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }
    }
}
