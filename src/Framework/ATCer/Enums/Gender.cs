// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Attributes;
using System.ComponentModel;

namespace ATCer.Enums
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        [TagColor(ClientAntPresetColor.Blue)]
        Male,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        [TagColor(ClientAntPresetColor.Magenta)]
        Female
    }
}