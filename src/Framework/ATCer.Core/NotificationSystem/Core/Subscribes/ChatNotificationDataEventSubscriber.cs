// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.EventBus;
using ATCer.EventBus;
using ATCer.NotificationSystem.Dtos.Notification;
using ATCer.NotificationSystem.Enums;
using ATCer.NotificationSystem.Services;

namespace ATCer.NotificationSystem.Core.Subscribes
{
    /// <summary>
    /// 系统通知事件订阅
    /// </summary>
    public class ChatNotificationDataEventSubscriber : IEventSubscriber, ISingleton
    {
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemNotificationService"></param>
        public ChatNotificationDataEventSubscriber(ISystemNotificationService systemNotificationService)
        {
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 聊天数据
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventType.SystemNotify) + nameof(NotificationDataType.Chat))]
        public async Task Chat(EventHandlerExecutingContext context)
        {
            IEventSource eventSource = context.Source;
            ChatDemoNotificationData chatNotification = (ChatDemoNotificationData)eventSource.Payload;

            //收到聊天消息，转发给所有客户端
            await systemNotificationService.SendToAllClient(chatNotification);
            ChatDemoService.AddChatMessage(chatNotification);
        }


    }
}
