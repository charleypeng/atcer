// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;
using ATCer.Email.Enums;
using System;

namespace ATCer.VerifyCode.Core.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailVerifyCodeOptions: VerifyCodeOptions, IConfigurableOptions
    {
        /// <summary>
        /// 邮件模板编号
        /// </summary>
        public Guid EmailTemplateId { get; set; }
        /// <summary>
        /// 邮件服务器标签
        /// </summary>
        public EmailServerTag EmailServerTag { get; set; } = EmailServerTag.Base;
    }
}
