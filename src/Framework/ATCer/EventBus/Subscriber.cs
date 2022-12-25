// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace ATCer.EventBus
{
    /// <summary>
    /// 订阅者
    /// </summary>
    public class Subscriber
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public Type EventType { get; set; }
        /// <summary>
        /// 事件处理方法
        /// </summary>
        public Func<EventBase,Task> CallBack { get; set; }
        /// <summary>
        /// 订阅者编号
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 订阅者
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="callBack"></param>
        public Subscriber(Type eventType, Func<EventBase, Task> callBack)
        {
            Id = Guid.NewGuid().ToString();
            EventType = eventType;
            CallBack = callBack;
        }
    }
}
