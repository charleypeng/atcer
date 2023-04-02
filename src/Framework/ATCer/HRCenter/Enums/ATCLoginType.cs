// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.HRCenter.Enums;

/// <summary>
/// 登入|登出类型
/// </summary>
public enum ATCLoginType:byte
{
    /// <summary>
    /// 指纹
    /// </summary>
    [Description("指纹")]
    FingerPrint,
    /// <summary>
    /// 交换席位
    /// </summary>
    [Description("交换席位")]
    Exchange,
    /// <summary>
    /// 系统
    /// </summary>
    [Description("系统")]
    System,
    /// <summary>
    /// 覆盖
    /// </summary>
    [Description("覆盖")]
    Converge,
    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    Unknown
}