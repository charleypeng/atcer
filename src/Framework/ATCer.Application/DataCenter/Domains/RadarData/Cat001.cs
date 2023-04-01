// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Models;
namespace ATCer.DataCenter.Domains.RadarData;

/// <summary>
/// Asterix CAT001
/// </summary>
public class Cat001 : BaseRadarDomain
{
    public Cat001_I020? I020 { get; set; }
    public Cat001_I070? I070 { get; set; }
    public Cat001_I030? I030 { get; set; }
    public Cat001_I090? I090 { get; set; }
    public Cat001_I161? I161 { get; set; }
    public Cat001_I050? I050 { get; set; }
}