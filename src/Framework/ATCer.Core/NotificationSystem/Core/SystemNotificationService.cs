// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.NotificationSystem.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace ATCer.NotificationSystem.Core
{
    /// <summary>
    /// 系统通知服务
    /// </summary>
    public class SystemNotificationService : ISystemNotificationService
    {
        private readonly IHubContext<SystemNotificationHub> hubContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubContext"></param>
        public SystemNotificationService(IHubContext<SystemNotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }


        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public async Task SendToAllClient(NotificationData notifyData)
        {
            string json= System.Text.Json.JsonSerializer.Serialize(notifyData,notifyData.GetType());
            await hubContext.Clients.All.SendAsync("ReceiveMessage", json);
        }

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public async Task SendToUser(int userId, NotificationData notifyData)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(notifyData);
            await hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveMessage", json);
        }
    }
}
