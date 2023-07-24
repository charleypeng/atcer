// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using ATCer.HRCenter.Enums;
using System.Text.Json.Serialization;

namespace ATCer.HRCenter.Dtos
{
    /// <summary>
    /// 执勤时间
    /// </summary>
    [DisplayName("执勤时间")]
    public class TimeItemDto: ATCerBaseDto<long>
    {
        /// <summary>
        /// 管制员Id
        /// </summary>
        [Description("管制员Id")]
        public int UserATCInfoId { get; set; }
        /// 扇区Id
        /// </summary>
        [Description("扇区Id")]
        public int SectorId { get; set; }
        /// <summary>
        /// 登入方式
        /// </summary>
        [Description("登入方式")]
        public ATCLoginType TypeOfLogin { get; set; }
        /// <summary>
        /// 登出方式
        /// </summary>
        [Description("登出方式")]
        public ATCLoginType TypeOfLogout { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Description("开始时间")]
        public DateTimeOffset BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Description("结束时间")]
        public DateTimeOffset EndTime { get; set; }
        /// <summary>
        /// 夜班时间Id
        /// </summary>
        [Description("夜班时间Id")]
        public int WorkTimeConfId { get; set; }
        /// <summary>
        /// 管制员角色
        /// </summary>
        [DisplayName("角色")]
        public ControllerRole ControllerRole { get; set; }
        /// <summary>
        /// 是否确认导入
        /// </summary>
        [DisplayName("导入确认")]
        public bool Confirmed { get; set; }

        /// <summary>
        /// 山区信息
        /// </summary>
        [DisplayName("管制信息")]
        public SectorDto? Sector { get; set; }

        /// <summary>
        /// 管制员信息
        /// </summary>
        [DisplayName("扇区信息")]
        public UserATCInfoDto? UserATCInfo { get; set; }

    }
}
