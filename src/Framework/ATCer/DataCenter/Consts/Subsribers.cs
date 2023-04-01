// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Consts;

/// <summary>
/// 订阅者
/// </summary>
public struct Subsribers
{
    /// <summary>
    /// 气象数据订阅
    /// </summary>
    public struct MetData
    {

    }

    /// <summary>
    /// 雷达数据订阅
    /// </summary>
    public struct RadarData
    {
        public const string RawCat048 = "data.raw.radar.cat048";
    }
    /// <summary>
    /// 
    /// </summary>
    public struct RadarSorceType
    {
        public const string CAT001 = "data.raw.c001";
    }
}
