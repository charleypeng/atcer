// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer.VerifyCode.Dtos
{
    /// <summary>
    /// 验证码校验输入
    /// </summary>
    public abstract class VerifyCodeCheckInput: VerifyCodeBase
    {
        /// <summary>
        /// 验证码Key
        /// </summary>
        [Required(ErrorMessage = "验证码Key不能为空。")]
        [DisplayName("验证码Key")]
        public string VerifyCodeKey { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不能为空。")]
        [DisplayName("验证码")]
        public string VerifyCode { get; set; }
    }
}
