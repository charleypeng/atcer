// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.EventBus;
using System.Threading.Tasks;

namespace ATCer.Client.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public abstract class EventSubscriberBase<TEvent> : IEventSubscriber where TEvent : EventBase
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="e"></param>
        public abstract Task CallBack(TEvent e);

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Task CallBack(object e)
        {
            //类型不同，无法转换，返回
            if (!e.GetType().Equals(typeof(TEvent))) 
            {
                return Task.CompletedTask;
            }
            return CallBack((TEvent)e);
        }
    }

    
}
