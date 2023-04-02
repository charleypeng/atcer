// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

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
    InCommand,
    /// <summary>
    /// 监控席
    /// </summary>
    [Description("监控席")]
    CoCommand,
    /// <summary>
    /// 协调席
    /// </summary>
    [Description("协调席")]
    Coordinate,
    /// <summary>
    /// 计划席
    /// </summary>
    [Description("计划席")]
    Plan,
    /// <summary>
    /// 报告席
    /// </summary>
    [Description("报告席")]
    Report,
    /// <summary>
    /// 流控席
    /// </summary>
    [Description("流控席")]
    FlowControl,
    /// <summary>
    /// 带班席
    /// </summary>
    [Description("领班席")]
    Supervisor,
    /// <summary>
    /// 运行监控
    /// </summary>
    [Description("运行监控")]
    CoSupervisor
}