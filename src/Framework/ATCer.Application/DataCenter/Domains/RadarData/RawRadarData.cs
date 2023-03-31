// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Domains.RadarData;

/// <summary>
/// 原始雷达数据
/// </summary>
public class RawRadarData
{
    /// <summary>
    /// 雷达数据类型
    /// </summary>
    public string? tp { get; set; }
    /// <summary>
    /// 雷达
    /// </summary>
    public string? H { get; set; }
    /// <summary>
    /// 时间
    /// </summary>
    public double? T { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    public List<object>? D { get; set; }
    /// <summary>
    /// 源数据
    /// </summary>
    public string? O { get; set; }
}
