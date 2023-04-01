// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Enums.RadarData;
using System.Text.Json.Serialization;

namespace ATCer.DataCenter.Models;

public class Cat001_I020
{
    /// <summary>
    /// 航迹类型
    /// <para>0:Plot</para>
    /// <para>1:Track</para>
    /// </summary>
    public byte? TYP { get; set; }
    public byte? SIM { get; set; }
    public SSRPSR? SSRPSR { get; set; }
    public byte? ANT { get; set; }
    public byte? SPI { get; set; }
    public byte? RAB { get; set; }
    public byte? TST { get; set; }
    /// <summary>
    /// 应答机状态
    /// </summary>
    public DS1DS2? DS1DS2 { get; set; }

    /// <summary>
    /// 空军紧急情况
    /// </summary>
    public byte? ME { get; set; }
    /// <summary>
    /// 空军识别
    /// </summary>
    public byte? MI { get; set; }
}

public class Cat001_I070
{
    public byte? V { get; set; }
    public byte? G { get; set; }
    public byte? L { get; set; }
    public int? Mode3A { get; set; }

}

public class Cat001_I030
{
    public byte? WE { get; set; }
}

public class Cat001_I090
{
    public byte? V { get; set; }
    public byte? G { get; set; }
    public float? ModeC { get; set; }
}

public class Cat001_I161
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("tpn")]
    public int? TPN { get; set; }
}

public class Cat001_I050
{
    public byte? V { get; set; }
    public byte? G { get; set; }
    public byte? L { get; set; }
    public float? Mode2 { get; set; }
}
