// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.Client.Base.EventBus.Events;

namespace ATCer.NotificationSystem.Client.Subscribes
{
    /// <summary>
    /// 刷新token后
    /// </summary>
    [TransientService]
    public class RefreshTokenSucceedAfterEventSubscriber : EventSubscriberBase<RefreshTokenSucceedAfterEvent>
    {
        public override Task CallBack(RefreshTokenSucceedAfterEvent e)
        {
            return Task.CompletedTask;
        }
    }
}
