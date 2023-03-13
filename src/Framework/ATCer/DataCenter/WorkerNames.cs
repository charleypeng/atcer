// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Enums;

namespace ATCer.DataCenter;

/// <summary>
/// 
/// </summary>
public struct WorkerNames
{
    /// <summary>
    /// 气压数据订阅者
    /// </summary>
    public const string Met_Raw_Press =  Met_Raw_Prefix + "press";
    /// <summary>
    /// 
    /// </summary>
    public const string Met_Raw_Prefix = "datacenter.met.raw.";
}