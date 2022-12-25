// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Enums;
using System;

namespace ATCer.Attributes
{
    /// <summary>
    /// 标签颜色特性
    /// </summary>
    public class TagColorAttribute : Attribute
    {
        /// <summary>
        /// 标签颜色特性
        /// </summary>
        /// <param name="color"></param>
        public TagColorAttribute(ClientAntPresetColor presetColor)
        {
            PresetColor = presetColor;
        }

        /// <summary>
        /// 标签颜色特性
        /// </summary>
        /// <param name="color"></param>
        public TagColorAttribute(string color)
        {
            Color = color;
        }

        /// <summary>
        /// 预设颜色
        /// </summary>
        public ClientAntPresetColor? PresetColor { get; private set; }

        /// <summary>
        /// 自定义颜色
        /// </summary>
        /// <remarks>
        /// 优先级高于PresetColor
        /// </remarks>
        public string Color { get; private set; }
    }
}
