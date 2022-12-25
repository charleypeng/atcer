// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Enums;
using System;
using System.ComponentModel;
using ATCer.Base;

namespace ATCer.HRCenter.Dtos
{
    /// <summary>
    /// 计算执勤时间Dto
    /// </summary>
    public class CalTimeItemDto : ATCerPureDto<long>
    {
        /// <summary>
        /// 扇区名称
        /// </summary>
        [DisplayName("扇区名称")]
        public string SectorName { get; set; }
        /// <summary>
        /// 扇区代码
        /// </summary>
        [DisplayName("扇区名称")]
        public string SectorCode { get; set; }
        /// <summary>
        /// 扇区席位
        /// </summary>
        [DisplayName("扇区名称")]
        public PositionRole PositionRole { get; set; }
        /// <summary>
        /// 管制员角色
        /// </summary>
        [DisplayName("角色")]
        public ControllerRole ControllerRole { get; set; }
        /// <summary>
        /// 登入方式
        /// </summary>
        [DisplayName("登入方式")]
        public ATCLoginType TypeOfLogin { get; set; }
        /// <summary>
        /// 登出方式
        /// </summary>
        [DisplayName("登出方式")]
        public ATCLoginType TypeOfLogout { get; set; }
        ///// <summary>
        ///// 开始时间
        ///// </summary>
        //[DisplayName("开始时间")]
        //public DateTimeOffset BeginTime { get; set; }
        ///// <summary>
        ///// 结束时间
        ///// </summary>
        //[DisplayName("结束时间")]
        //public DateTimeOffset EndTime { get; set; }
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
        public double NightShiftMultiplier { get; set; }
        /// <summary>
        ///管制员角色系数
        /// </summary>
        [DisplayName("角色系数")]
        public double ControllerRoleMultiplier { get; set; }
    }
}
