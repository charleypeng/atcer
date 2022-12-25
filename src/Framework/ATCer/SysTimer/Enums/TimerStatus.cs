// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.SysTimer.Enums
{
    /// <summary>
    /// 任务状态
    /// </summary>    
    public enum TimerStatus
    {
        /// <summary>
        /// 运行中
        /// </summary>        
        [Description("运行中")]
        Running,
        /// <summary>
        /// 已停止或未启动
        /// </summary>        
        [Description("已停止")]
        Stopped,
        /// <summary>
        /// 单次执行失败
        /// </summary>        
        [Description("失败")]
        Failed,
        /// <summary>
        /// 任务已取消或没有该任务
        /// </summary>        
        [Description("不存在")]
        CanceledOrNone
    }
}
