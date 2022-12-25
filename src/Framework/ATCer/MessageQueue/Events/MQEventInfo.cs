// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.MessageQueue.Events
{
    /// <summary>
    /// 事件消息类
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class MQEventInfo<TData>:ATCer.EventBus.EventBase
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        public MQEventInfo(string topic, TData data)
        {
            EventTopic = topic;
            Data = data;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public TData Data { get; set; }
        /// <summary>
        /// 订阅事件
        /// </summary>
        public string EventTopic { get; set; }
    }
}
