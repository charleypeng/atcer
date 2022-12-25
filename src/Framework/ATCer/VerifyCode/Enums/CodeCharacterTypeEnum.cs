// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.ImageVerifyCode.Enums
{
    /// <summary>
    /// 校验码字符类别，有数字，字符，数字加字符
    /// </summary>
    public enum CodeCharacterTypeEnum
    {
        /// <summary>
        /// 数字
        /// </summary>
        Number = 1,

        /// <summary>
        /// 字符
        /// </summary>
        Character = 2,

        /// <summary>
        /// 数字加字符
        /// </summary>
        NumberAndCharacter = 3,
    }
}
