// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.Client.Base.EventBus.Events;
using System.Threading.Tasks;

namespace ATCer.Client.Core.Subscribes
{
    /// <summary>
    /// 登出后端口系统通知
    /// </summary>
    [TransientService]
    public class LogoutSucceedAfterEventSubscriber : EventSubscriberBase<LogoutSucceedAfterEvent>
    {
        private readonly ISignalRClientManager signalRClientManager;

        public LogoutSucceedAfterEventSubscriber(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }

        public override Task CallBack(LogoutSucceedAfterEvent e)
        {
            return signalRClientManager.StopAll();
        }
    }
}
