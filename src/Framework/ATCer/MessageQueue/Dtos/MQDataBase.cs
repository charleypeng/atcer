// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.MessageQueue.Dtos
{
    /// <summary>
    /// MQ基类
    /// </summary>
    public abstract class MQDataBase
    {
        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTimeOffset IssueTime { get; set; } = DateTimeOffset.Now;
    }
}
