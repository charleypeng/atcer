// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Attributes;
using ATCer.Base;
using ATCer.VerifyCode.Enums;
using System;
using System.ComponentModel;

namespace ATCer.VerifyCode.DbStore.Domain
{
    /// <summary>
    /// 验证码
    /// </summary>
    [Description("验证码")]
    [IgnoreAudit]
    public class VerifyCodeLog : ATCerEntityBase<Guid>
    {
        /// <summary>
        /// 验证码类型
        /// </summary>
        [DisplayName("验证码类型")]
        public VerifyCodeTypeEnum VerifyCodeType { get; set; }
        /// <summary>
        /// 验证码唯一键
        /// </summary>
        [DisplayName("验证码唯一键")]
        public string Key { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [DisplayName("验证码")]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        [DisplayName("过期时间")]
        public DateTimeOffset EndTime { get; set; }
    }
}
