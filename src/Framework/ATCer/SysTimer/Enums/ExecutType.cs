// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.SysTimer.Enums
{
    /// <summary>
    /// 执行模式
    /// </summary>
    public enum ExecutMode
    {
        /// <summary>
        /// 并行执行（不会等到上一个任务完成）
        /// </summary>
        [Description("并行")]
        Parallel = 0,
        /// <summary>
        /// 串行执行（等到上一个任务完成后执行）
        /// </summary>
        [Description("串行")]
        Scceeding = 1
    }
}
