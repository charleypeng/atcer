// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataCenter.Enums
{
    /// <summary>
    /// 数据状态编码
    /// </summary>
    public enum MetDataStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        /// <remarks>
        /// (N)数据来自于传感器并且是有效的
        /// </remarks>
        [Description("正常")]
        Normal = 0,
        /// <summary>
        /// 人工
        /// </summary>
        /// <remarks>
        /// 数据是有效的，但是由观测员输入系统。
        /// </remarks>
        [Description("人工")]
        Manual = 1,
        /// <summary>
        /// 备份
        /// </summary>
        /// <remarks>
        /// (C)数据来自于备份传感器
        /// </remarks>
        [Description("备份")]
        Copied = 2,
        /// <summary>
        /// 旧值
        /// </summary>
        /// <remarks>
        ///(O)数据到了某个超时阶段还未被更新，但仍然认为其是有效数据
        /// </remarks>
        [Description("旧值")]
        Old = 3,
        /// <summary>
        /// 丢失
        /// </summary>
        /// <remarks>
        /// (M)数据还没更新且已被认为丢失
        /// </remarks>
        [Description("丢失")]
        Missing = 4,
        /// <summary>
        /// 不可用
        /// </summary>
        /// <remarks>
        /// (I)数据传感器故障或超出范围，已经不能被使用
        /// </remarks>
        [Description("不可用")]
        Invalid = 5,
        /// <summary>
        /// 未知
        /// </summary>
        /// <remarks>
        /// (U)数据的值可能是有效的，但是不能被使用
        /// </remarks>
        [Description("未知")]
        Undefined = 6,
    }

    /// <summary>
    /// 
    /// </summary>
    public struct MetDataStatusDict
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, MetDataStatus> Dict
        {
            get => new Dictionary<string, MetDataStatus>()
                {
                    {"N", MetDataStatus.Normal},
                    {"M", MetDataStatus.Manual},
                    {"C", MetDataStatus.Copied},
                    {"O", MetDataStatus.Old},
                    {"-", MetDataStatus.Missing},
                    {"I", MetDataStatus.Invalid},
                    {"U", MetDataStatus.Undefined},
                };
        }
    }
}
