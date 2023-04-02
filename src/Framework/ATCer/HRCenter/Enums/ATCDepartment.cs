// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

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
    TWR = 0,
    /// <summary>
    /// 进近
    /// </summary>
    [Description("进近")]
    APP = 1,
    /// <summary>
    /// 区调
    /// </summary>
    [Description("区调")]
    ACC = 2,
    /// <summary>
    /// 非管制部门
    /// </summary>
    [Description("非管制部门")]
    NONE = 3,
}
