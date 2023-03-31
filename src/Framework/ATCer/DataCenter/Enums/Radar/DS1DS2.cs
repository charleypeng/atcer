// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Enums.RadarData;

/// <summary>
/// 
/// </summary>
public enum DS1DS2 : byte
{
    /// <summary>
    /// 默认
    /// </summary>
    Default = 0,
    /// <summary>
    /// 非法干扰
    /// <para>A7500</para>
    /// </summary>
    UFI = 1,
    /// <summary>
    /// 通讯失效
    /// <para>A7600</para>
    /// </summary>
    RCF = 2,
    /// <summary>
    /// 紧急情况
    /// <para>A7700</para>
    /// </summary>
    EMG = 3
}