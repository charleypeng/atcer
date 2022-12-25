// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using System.Net;
using System.Threading.Tasks;

namespace ATCer.Client.Core.EventBus.Subscribes
{
    /// <summary>
    /// 
    /// </summary>
    [TransientService]
    public class UnauthorizedApiCallEventSubscriber : EventSubscriberBase<UnauthorizedApiCallEvent>
    {

        private readonly IAuthenticationStateManager authenticationStateManager;

        public UnauthorizedApiCallEventSubscriber(IAuthenticationStateManager authenticationStateManager)
        {
            this.authenticationStateManager = authenticationStateManager;
        }

        public override async Task CallBack(UnauthorizedApiCallEvent e)
        {
            if (e.HttpStatusCode.Equals(HttpStatusCode.Unauthorized)) 
            {
                await authenticationStateManager.RefreshToken();
            }
        }
    }
}
