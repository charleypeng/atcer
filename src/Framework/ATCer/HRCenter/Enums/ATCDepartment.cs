// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Enums;

namespace ATCer.HRCenter.Enums;

/// <summary>
/// 管制部门
/// </summary>
public enum ATCDepartment:byte
{
    /// <summary>
    /// 塔台
    /// </summary>
    [Description("塔台")]
    [TagColor(ClientAntPresetColor.Blue)]
    TWR = 0,
    /// <summary>
    /// 进近
    /// </summary>
    [Description("进近")]
    [TagColor(ClientAntPresetColor.Green)]
    APP = 1,
    /// <summary>
    /// 区调
    /// </summary>
    [Description("区调")]
    [TagColor(ClientAntPresetColor.Purple)]
    ACC = 2,
    /// <summary>
    /// 非管制部门
    /// </summary>
    [Description("非管制部门")]
    NONE = 3,
}
