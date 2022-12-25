// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Common;
using ATCer.MessageQueue.Dtos;
using ATCer.MessageQueue.Events;
using ATCer.EventBus;

namespace ATCer.Client.Core
{
    [ScopedService]
    public class MQSingalRProvider : ISignalRClientProvider
    {
        private readonly ISignalRClientBuilder signalRClientBuilder;
        private readonly IEventBus _eventBus;
        public MQSingalRProvider(ISignalRClientBuilder signalRClientBuilder,
                                 IEventBus eventBus)
        {
            this.signalRClientBuilder = signalRClientBuilder;
            _eventBus = eventBus;
        }

        public ISignalRClient GetSignalRClient()
        {
            ISignalRClient signalRClient = signalRClientBuilder
                .GetInstance()
                .SetClientName(SingalRClientSchemes.DefaultSingalRClientName)
                .SetUrl("ws/mq")
                .Build();

            signalRClient.On<MQData>(MQMethods.MQSendAsync, CallBack);

            return signalRClient;
        }
        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        private Task CallBack(MQData data)
        {
            //注册接收调用方法
            if (data == null)
            {
                return Task.CompletedTask;
            }
            //解析为基本通知事件
            MQEventInfo<MQData> eventInfo = new MQEventInfo<MQData>(data.MQTopic, data);
            return _eventBus.Publish(eventInfo);
        }
    }
}

