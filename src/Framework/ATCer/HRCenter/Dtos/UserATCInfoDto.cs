// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ATCer.HRCenter.Enums;
using ATCer.Base;

namespace ATCer.HRCenter.Dtos
{
    /// <summary>
    /// 管制员模型
    /// </summary>
    public class UserATCInfoDto: ATCerBaseDto<int>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Description("用户ID")]
        [Required]
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

    }
}
