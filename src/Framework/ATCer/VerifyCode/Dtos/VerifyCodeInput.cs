// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.VerifyCode.Dtos
{
    /// <summary>
    /// 验证码输入
    /// </summary>
    public abstract class VerifyCodeInput: VerifyCodeBase
    {
        /// <summary>
        /// 创建code参数
        /// 不穿时使用配置
        /// </summary>
        public CharacterCodeCreateParam CreateCodeParam { get; set; }
    }
}
