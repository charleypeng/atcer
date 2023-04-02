// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.HRCenter.Options;

/// <summary>
/// 小时费计算选项
/// </summary>
public class TimeCalculateOptions
{
    /// <summary>
    /// 白班时间
    /// </summary>
    public TimeSpan DaySpan { get; set; } = new TimeSpan(8, 0, 0);
    /// <summary>
    /// 夜班时间
    /// </summary>
    public TimeSpan NightSpan { get; set; } = new TimeSpan(22, 0, 0);
}
