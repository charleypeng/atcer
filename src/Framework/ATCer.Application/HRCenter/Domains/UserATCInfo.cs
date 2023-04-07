// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ATCer.HRCenter.Enums;

#nullable disable
namespace ATCer.HRCenter.Domains
{
    /// <summary>
    /// 管制员信息
    /// </summary>
    [Comment("管制员信息")]
    public class UserATCInfo : ATCerEntityBase, IEntitySeedData<UserATCInfo>, IBaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Description("用户ID")]
        public int UserId { get; set; }
        /// <summary>
        /// 管制员名称
        /// </summary>
        [Description("管制员名称")]
        [Required]
        public string ATCName { get; set; }
        /// <summary>
        /// 管制员等级
        /// </summary>
        [Description("管制员等级")]
        public ATCLevel ATCLevel { get; set; }
        /// <summary>
        /// 管制部门
        /// </summary>
        [Description("管制部门")]
        public ATCDepartment Department { get; set; }
        /// <summary>
        /// 执照获得时间
        /// </summary>
        [Description("执照获得时间")]
        public DateTimeOffset? LicenseGetDate { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Description("执照过期时间")]
        public DateTimeOffset? LicenseExpireDate { get; set; }
        /// <summary>
        /// ICAO初次获得时间
        /// </summary>
        [Description("ICAO执照初次获得时间")]
        public DateTimeOffset? ICAOFirstLicenseDate { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Description("ICAO执照过期时间")]
        public DateTimeOffset? ICAOLicenseGetDate { get; set; }
        /// <summary>
        /// 管制职位
        /// </summary>
        public ControllerRole Role { get; set; }
        /// <summary>
        /// 是否三类人员
        /// </summary>
        [Description("是否三类人员")]
        public bool IsCat3People { get; set; }
        /// <summary>
        /// Seed data
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<UserATCInfo> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new UserATCInfo[]
            {
                new UserATCInfo{Id=1, ATCName = "彭磊", UserId=1, ATCLevel=ATCLevel.SanJi, Department=ATCDepartment.TWR }
            };
        }
    }
}
