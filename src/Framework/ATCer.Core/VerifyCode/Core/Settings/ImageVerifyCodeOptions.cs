// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace ATCer.VerifyCode.Core.Settings
{
    /// <summary>
    /// 图片验证码
    /// </summary>
    public class ImageVerifyCodeOptions : VerifyCodeOptions, IConfigurableOptions
    {
        
        /// <summary>
        /// 校验码字体大小（默认18）
        /// </summary>
        public int CodeFontSize { get; set; } = 18;
    }
}
