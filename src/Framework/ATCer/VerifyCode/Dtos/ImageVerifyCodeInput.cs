// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.VerifyCode.Enums;

namespace ATCer.VerifyCode.Dtos
{
    /// <summary>
    /// 图片验证码输入
    /// </summary>
    public class ImageVerifyCodeInput : VerifyCodeInput
    {
        /// <summary>
        /// 校验码字体大小
        /// </summary>
        public int? FontSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public override VerifyCodeTypeEnum VerifyCodeType => VerifyCodeTypeEnum.Image;
    }
}
