// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.SysTimer.Enums
{
    /// <summary>
    /// 任务类型
    /// </summary>
    public enum TimerTypes
    {
        /// <summary>
        /// 间隔方式
        /// </summary>        
        [Description("间隔方式")]
        Interval,
        /// <summary>
        /// Cron 表达式
        /// </summary>        
        [Description("Cron 表达式")]
        Cron
    }
}
