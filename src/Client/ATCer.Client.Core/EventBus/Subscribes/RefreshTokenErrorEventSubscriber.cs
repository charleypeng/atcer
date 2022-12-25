// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using System.Threading.Tasks;

namespace ATCer.Client.Core.EventBus.Subscribes
{
    /// <summary>
    /// 刷新token失败事件处理器
    /// </summary>
    [TransientService]
    public class RefreshTokenErrorEventSubscriber : EventSubscriberBase<RefreshTokenErrorEvent>
    {
        private readonly IAuthenticationStateManager authenticationStateManager;

        public RefreshTokenErrorEventSubscriber(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        public override async Task CallBack(RefreshTokenErrorEvent e)
        {
            await authenticationStateManager.CleanUserInfo();
        }
    }
}
