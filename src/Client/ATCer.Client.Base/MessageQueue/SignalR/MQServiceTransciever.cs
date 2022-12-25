// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.MessageQueue.Dtos;
using ATCer.Common;

namespace ATCer.Client.Base
{
    /// <summary>
    /// MQ消息发送器
    /// </summary>
    [ScopedService]
    public class MQServiceTransciever
    {
        private readonly ISignalRClientManager _signalRClientManager;
        private readonly IAuthenticationStateManager _stateManager;
        public MQServiceTransciever(ISignalRClientManager signalRClientManager, IAuthenticationStateManager stateManager)
        {
            _signalRClientManager = signalRClientManager;
            _stateManager = stateManager;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task Send<TData>(TData data)
        {
            var mqData = new MQData();
            string strData = string.Empty;
            try
            {
                strData = System.Text.Json.JsonSerializer.Serialize(data);
            }
            catch (Exception)
            {
                Console.WriteLine($"数据错误：{data}");
            }
            finally
            {
                mqData.Data = strData;
                await _signalRClientManager.Get(SingalRClientSchemes.DefaultSingalRClientName)
                                           .SendAsync(HubMethods.Send, mqData);
            }
            
        }

        public async Task Send<TData>(TData data, string topic, string callback = null)
        {
            var mqData = new MQData();
            string strData = string.Empty;
            try
            {
                strData = System.Text.Json.JsonSerializer.Serialize(data);
            }
            catch (Exception)
            {
                Console.WriteLine($"数据错误：{data}");
            }
            finally
            {
                mqData.Data = strData;
                //set the default topic or use parameter
                mqData.MQTopic = string.IsNullOrWhiteSpace(topic) ? MQTopics.MQCenter.MQSendToAll : topic;
                //set mq data
                mqData.MQTopic = topic;
                mqData.CallBack = callback;
                await _signalRClientManager.Get(SingalRClientSchemes.DefaultSingalRClientName)
                                           .SendAsync(HubMethods.Send, mqData);
            }

        }
    }
}
