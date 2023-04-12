// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using ATCer.HRCenter.Enums;
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ATCer.HRCenter.Dtos
{
    /// <summary>
    /// 执勤时间
    /// </summary>
    [DisplayName("执勤时间")]
    public class TimeItemDto: ATCerBaseDto<long>, IEquatable<TimeItemDto>
    {
        /// <summary>
        /// 管制员
        /// </summary>
        [DisplayName("管制员")]
        public string UserName { get; set; }
        /// <summary>
        /// 席位名称
        /// </summary>
        [DisplayName("席位名称")]
        public string SectorName { get; set; }
        /// <summary>
        /// 管制员Id
        /// </summary>
        [DisplayName("Id")]
        public int UserId { get; set; }
        /// <summary>
        /// 扇区Id
        /// </summary>
        [DisplayName("扇区")]
        public int SectorId { get; set; }
        /// <summary>
        /// 登入方式
        /// </summary>
        [DisplayName("登入方式")]
        public ATCLoginType TypeOfLogin { get; set; } = ATCLoginType.Unknown;
        /// <summary>
        /// 登出方式
        /// </summary>
        [DisplayName("登出方式")]
        public ATCLoginType TypeOfLogout { get; set; } = ATCLoginType.Unknown;
        /// <summary>
        /// 开始时间
        /// </summary>
        [DisplayName("开始时间")]
        public DateTimeOffset BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [DisplayName("结束时间")]
        public DateTimeOffset EndTime { get; set; }
        /// <summary>
        /// 夜班时间Id
        /// </summary>
        [DisplayName("夜班时间Id")]
        public int WorkTimeConfId { get; set; }
        /// <summary>
        /// 岗位角色
        /// </summary>
        [DisplayName("角色")]
        public ControllerRole ControllerRole { get; set; }
        /// <summary>
        /// 是否确认导入
        /// </summary>
        [DisplayName("导入确认")]
        public bool Confirmed { get; set; }

        [JsonIgnore]
        public bool IsComparer { get; set; } = false;

        [JsonIgnore]
        public Guid GroupId { get; set; }

        public bool Equals(TimeItemDto? other)
        {
            return Id.Equals(other?.Id);
        }
    }
}
