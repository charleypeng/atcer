// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer.HRCenter.Domains
{
    /// <summary>
    /// 执勤信息
    /// </summary>
    [Comment("执勤信息")]
    public class TimeItem:ATCerEntityBase<long>, IBaseEntity, IEntityTypeBuilder<TimeItem>
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

        public Sector? Sector { get; set; }

        public UserATCInfo? UserATCInfo { get; set; }

        public void Configure(EntityTypeBuilder<TimeItem> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasOne(x => x.UserATCInfo)
                .WithMany(x => x.TimeItems)
                .HasForeignKey(x => x.UserATCInfoId);

            entityBuilder
                .HasOne(x => x.Sector)
                .WithMany(x => x.TimeItems)
                .HasForeignKey(x => x.SectorId);
        }
    }
}
