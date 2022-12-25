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
    /// 验证码返回结果
    /// </summary>
    public class VerifyCodeOutput
    {
        /// <summary>
        /// 验证码类型
        /// </summary>
        public virtual VerifyCodeTypeEnum VerifyCodeType { get; }
        /// <summary>
        /// 验证码唯一键
        /// </summary>
        [DisplayName("验证码唯一键")]
        public string Key { get; set; }
    }
}
