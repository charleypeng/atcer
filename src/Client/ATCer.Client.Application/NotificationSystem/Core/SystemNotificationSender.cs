// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.NotificationSystem.Dtos;

namespace ATCer.NotificationSystem.Client.Core
{
    [ScopedService]
    public class SystemNotificationSender
    {
        private readonly ISignalRClientManager signalRClientManager;

        public SystemNotificationSender(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }



        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        public Task Send(NotificationData notificationData)
        {
            ISignalRClient signalRClient = signalRClientManager.Get(NotificationSystemSignalRClientNames.SystemNotificationSignalRClientNames);
            return signalRClient.SendAsync("Send", notificationData);
        }
    }
}
