// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Enums.RadarData;

/// <summary>
/// 
/// </summary>
public enum SSRPSR : byte
{
    /// <summary>
    /// No detection
    /// </summary>
    ND = 0,
    /// <summary>
    /// Sole primary detection
    /// </summary>
    SPD = 1,
    /// <summary>
    /// Sole secondary detection
    /// </summary>
    SSD = 2,
    /// <summary>
    /// Combined primary and secondary detection
    /// </summary>
    CPSD = 3
}
