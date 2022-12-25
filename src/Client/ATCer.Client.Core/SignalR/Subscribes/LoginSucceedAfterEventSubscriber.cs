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
    /// 登录成功后，连接系统通知
    /// </summary>
    [TransientService]
    public class LoginSucceedAfterEventSubscriber : EventSubscriberBase<LoginSucceedAfterEvent>
    {
        private readonly ISignalRClientManager signalRClientManager;

        public LoginSucceedAfterEventSubscriber(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }

        public override Task CallBack(LoginSucceedAfterEvent e)
        {
            return signalRClientManager.ConnectionAndStartAll();
        }
    }
}
