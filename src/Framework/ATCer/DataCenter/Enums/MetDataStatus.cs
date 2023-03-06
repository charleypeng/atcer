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
        Normal = 0,
        /// <summary>
        /// 人工
        /// </summary>
        /// <remarks>
        /// 数据是有效的，但是由观测员输入系统。
        /// </remarks>
        Manual = 1,
        /// <summary>
        /// 正常
        /// </summary>
        /// <remarks>
        /// (N)数据来自于传感器并且是有效的
        /// </remarks>
        Copied = 2,
        /// <summary>
        /// 正常
        /// </summary>
        /// <remarks>
        /// (N)数据来自于传感器并且是有效的
        /// </remarks>
        Old = 3,
        /// <summary>
        /// 正常
        /// </summary>
        /// <remarks>
        /// (N)数据来自于传感器并且是有效的
        /// </remarks>
        Missing = 4,
        /// <summary>
        /// 正常
        /// </summary>
        /// <remarks>
        /// (N)数据来自于传感器并且是有效的
        /// </remarks>
        Invalid = 5,
        /// <summary>
        /// 正常
        /// </summary>
        /// <remarks>
        /// (N)数据来自于传感器并且是有效的
        /// </remarks>
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
