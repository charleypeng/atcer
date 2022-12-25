// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.VerifyCode.Enums;
using System.ComponentModel;

namespace ATCer.VerifyCode.Dtos
{
    /// <summary>
    /// 邮件验证码返回结果
    /// </summary>
    [Description("邮件验证码返回结果")]
    public class EmailVerifyCodeOutput : VerifyCodeOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public override VerifyCodeTypeEnum VerifyCodeType => VerifyCodeTypeEnum.Email;
    }
}
