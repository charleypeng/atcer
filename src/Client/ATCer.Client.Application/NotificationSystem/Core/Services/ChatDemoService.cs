// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.NotificationSystem.Dtos.Notification;
using ATCer.NotificationSystem.Services;

namespace ATCer.NotificationSystem.Client.Core.Services
{
    /// <summary>
    /// 聊天示例服务
    /// </summary>
    [ScopedService]
    public class ChatDemoService : IChatDemoService
    {
        private readonly string controller = "chat-demo";
        private readonly IApiCaller apiCaller;
        public ChatDemoService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }
        public async Task<IEnumerable<ChatDemoNotificationData >> GetHistory()
        {
            return await apiCaller.GetAsync<IEnumerable<ChatDemoNotificationData >>($"{controller}/history");
        }
    }
}
