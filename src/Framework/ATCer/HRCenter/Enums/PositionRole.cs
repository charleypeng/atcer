// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------
using ATCer.Enums;
namespace ATCer.HRCenter.Enums;

/// <summary>
/// 工作岗位
/// </summary>
public enum PositionRole:byte
{
    /// <summary>
    /// 指挥席
    /// </summary>
    [Description("指挥席")]
    [TagColor(ClientAntPresetColor.Red)]
    InCommand,
    /// <summary>
    /// 监控席
    /// </summary>
    [Description("监控席")]
    [TagColor(ClientAntPresetColor.GeekBlue)]
    CoCommand,
    /// <summary>
    /// 协调席
    /// </summary>
    [Description("协调席")]
    [TagColor(ClientAntPresetColor.Gold)]
    Coordinate,
    /// <summary>
    /// 计划席
    /// </summary>
    [Description("计划席")]
    [TagColor(ClientAntPresetColor.Volcano)]
    Plan,
    /// <summary>
    /// 报告席
    /// </summary>
    [Description("通报席")]
    [TagColor(ClientAntPresetColor.Green)]
    Report,
    /// <summary>
    /// 流控席
    /// </summary>
    [Description("流控席")]
    [TagColor(ClientAntPresetColor.Orange)]
    FlowControl,
    /// <summary>
    /// 带班席
    /// </summary>
    [Description("领班席")]
    [TagColor(ClientAntPresetColor.Cyan)]
    Supervisor,
    /// <summary>
    /// 运行监控
    /// </summary>
    [Description("运行监控")]
    [TagColor(ClientAntPresetColor.Pink)]
    CoSupervisor,
    /// <summary>
    /// 放行
    /// </summary>
    [Description("放行")]
    [TagColor(ClientAntPresetColor.Magenta)]
    ATCClearence,
}

public static class PositionRoleExtension
{
    public static PositionRole ToPositionRole(this string data)
    {
        var str = data.Replace("见习","").Replace("席","");
        if (string.IsNullOrWhiteSpace(str))
            throw new ArgumentNullException("position name");

        if (str.EndsWith("管制") || str.EndsWith("指挥"))
            return PositionRole.InCommand;
        else if (str.EndsWith("监控"))
            return PositionRole.CoCommand;
        else if (str.EndsWith("协调"))
            return PositionRole.Coordinate;
        else if (str.EndsWith("通报"))
            return PositionRole.Report;
        else if (str.EndsWith("流控") || str.EndsWith("流量") || str.EndsWith("AMAN-P"))
            return PositionRole.FlowControl;
        else if (str.EndsWith("计划"))
            return PositionRole.Plan;
        else if (str.EndsWith("领班"))
            return PositionRole.Supervisor;
        else if (str.EndsWith("运行监控"))
            return PositionRole.CoSupervisor;
        else if (str.EndsWith("放行"))
            return PositionRole.ATCClearence;
        else
            throw new Exception($"不存在的席位名称:{data}:inner:{str}");
    }
}