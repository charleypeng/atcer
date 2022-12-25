// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.VerifyCode.Enums;

namespace ATCer.VerifyCode.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class VerifyCodeBase
    {
        /// <summary>
        /// 验证码类型
        /// </summary>
        public abstract VerifyCodeTypeEnum VerifyCodeType { get; }
    }
}
