// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.SysTimer.Enums;
using System;

namespace ATCer.SysTimer.Dtos
{
    /// <summary>
    /// 任务方法信息
    /// </summary>
    public class TaskMethodInfo
    {
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 方法所属类的Type对象
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Type DeclaringType { get; set; }


        /// <summary>
        /// 方法所属类的Type名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 只执行一次
        /// </summary>
        public bool DoOnce { get; set; } = false;

        /// <summary>
        /// 立即执行（默认等待启动）
        /// </summary>
        public bool StartNow { get; set; } = false;

        /// <summary>
        /// 执行模式(并行、列队)
        /// </summary>
        public ExecutMode ExecuteMode { get; set; }

        /// <summary>
        /// 执行间隔时间（单位秒）
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        /// 定时器类型
        /// </summary>
        public TimerTypes TimerType { get; set; }

        /// <summary>
        /// 本地方法
        /// </summary>
        public string LocalMethod { get; set; }

        /// <summary>
        /// 执行类型
        /// </summary>
        /// <example>2</example>
        public ExecuteType ExecuteType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
