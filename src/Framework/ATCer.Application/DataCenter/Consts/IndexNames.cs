// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2023  版权所有
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Domains.MH4016_9;

namespace ATCer.DataCenter;

public class IndexNames
{
    public static string[] Names = new[] {"metdata.press","metdata.pv","metdata.vis"};

    public const string MetData_PRESS = "metdata.press";
    public const string MetData_CLOUD = "metdata.cloud";
    public const string MetData_PV = "metdata.pv";
    public const string MetData_VIS = "metdata.vis";
    public const string MetData_RwyLights = "metdata.rwylights";
    public const string MetData_PW= "metdata.pw";
    public const string MetData_HUMITEMP = "metdata.humitemp";
    public const string MetData_RAIN = "metdata.rain";
    public const string MetData_ROSA = "metdata.rosa";
    public const string MetData_WIND = "metdata.wind";
    /// <summary>
    /// 原始数据
    /// </summary>
    public const string MetData_Raw = "metdata.raw";
    public static Dictionary<string, Type> IndexDict
    {
        get => new Dictionary<string, Type>
        {
            {Names[0], typeof(PRESS)},
            {Names[1], typeof(PV)}
        };
    }
    
}