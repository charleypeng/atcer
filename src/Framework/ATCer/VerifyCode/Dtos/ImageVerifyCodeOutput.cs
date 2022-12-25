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
    /// 图片验证码返回结果
    /// </summary>
    [Description("图片验证码")]
    public class ImageVerifyCodeOutput : VerifyCodeOutput
    {
        /// <summary>
        /// Base64图片
        /// </summary>
        [DisplayName("Base64图片")]
        public string Base64Image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public override VerifyCodeTypeEnum VerifyCodeType => VerifyCodeTypeEnum.Image;
    }
}
