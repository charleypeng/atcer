// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.EventBus;
using System;
using System.Threading;

namespace ATCer.EventBus
{
    /// <summary>
    /// 基础事件源
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class EventSource<TBody> : IEventSource
    {
        /// <summary>
        /// 基础事件源
        /// </summary>
        /// <param name="eventId">事件唯一编号</param>
        public EventSource(string eventId)
        {
            this.eventId = eventId;
        }
        /// <summary>
        /// 事件唯一编号
        /// </summary>
        private string eventId;
        /// <summary>
        /// 内容
        /// </summary>
        public TBody Body { get; set; }

        public DateTime CreatedTime { get; } = DateTime.UtcNow;

        public CancellationToken CancellationToken { get; set; }

        public string EventId { get { return eventId; } }

        public object Payload { get { return Body; } }

    }
}
