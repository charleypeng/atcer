// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.EventBus;
using ATCer.NotificationSystem.Core;
using ATCer.NotificationSystem.Dtos;
using System.Text.Json;

namespace ATCer.NotificationSystem.Client.Core
{
    [ScopedService]
    public class SystemNotificationSignalRClientProvider : ISignalRClientProvider
    {
        private readonly IEventBus _eventBus;
        private readonly ISignalRClientBuilder signalRClientBuilder;
        private readonly IClientLogger clientLogger;

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="signalRClientBuilder"></param>
        public SystemNotificationSignalRClientProvider(IEventBus eventBus, ISignalRClientBuilder signalRClientBuilder, IClientLogger clientLogger)
        {
            _eventBus = eventBus;
            this.signalRClientBuilder = signalRClientBuilder;
            jsonSerializerOptions.Converters.Add(new NotificationDataJsonConverter());
            jsonSerializerOptions.IncludeFields = true;
            this.clientLogger = clientLogger;
        }

        public ISignalRClient GetSignalRClient()
        {
            ISignalRClient signalRClient = signalRClientBuilder
                .GetInstance()
                .SetClientName(NotificationSystemSignalRClientNames.SystemNotificationSignalRClientNames)
                .SetUrl("ws/system-notification")
                .Build();

            signalRClient.On<string>("ReceiveMessage", CallBack);

            return signalRClient;
        }
        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        private Task CallBack(string json)
        {
            try
            {
                NotificationData notificationData = JsonSerializer.Deserialize<NotificationData>(json, jsonSerializerOptions);
                //注册接收调用方法
                if (notificationData == null)
                {
                    return Task.CompletedTask;
                }
                return _eventBus.Publish(notificationData);
            }
            catch (Exception ex) {
                clientLogger.Error("Notification System CallBack Error", ex:ex, sendNotify:false);
                return Task.CompletedTask;
            }
        }

    }
}
