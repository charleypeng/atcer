// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ATCer.EventBus
{ 
    /// <summary>
    /// 事件服务
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancellationToken"></param>
        Task Publish(EventBase e, CancellationToken? cancellationToken=null);

        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="callBack"></param>
        /// <returns></returns>
        Subscriber Subscribe<TEvent>(Func<TEvent,Task> callBack) where TEvent : EventBase;

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="subscriber"></param>
        void UnSubscribe(Subscriber subscriber);
    }
}
