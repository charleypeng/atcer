// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Enums;

namespace ATCer.VerifyCode.Dtos
{
    /// <summary>
    /// 字符验证码参数
    /// </summary>
    public class CharacterCodeCreateParam 
    {
        /// <summary>
        /// 校验码字符数量
        /// </summary>
        public int? CharacterCount { get; set; }

        /// <summary>
        /// 校验码类别,有三种类别可选，分别是纯数字，纯字符，数字加字符
        /// </summary>
        public CodeCharacterTypeEnum? Type { get; set; }
    }
}
