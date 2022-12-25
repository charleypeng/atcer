// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.EventBus
{
    /// <summary>
    /// 事件消息
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class EventInfo<TData> : EventBase
    {
        /// <summary>
        /// 事件消息
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="data"></param>
        public EventInfo(EventType eventType, TData data)
        { 
            EventType = eventType;
            Data= data;
        
        }
        
        /// <summary>
        /// 消息
        /// </summary>
        public TData Data { get; set; }

    }
}
