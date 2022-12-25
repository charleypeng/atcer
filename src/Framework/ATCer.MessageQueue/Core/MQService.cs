// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.MessageQueue.Dtos;
using ATCer.Authentication.Dtos;

namespace ATCer.MessageQueue.Core
{
    /// <summary>
    /// Message queue service
    /// </summary>
    public class MQService : IMQService
    {
        private readonly IHubContext<MQServiceHub> hubContext;
        private readonly ICapPublisher publisher;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="publisher"></param>
        public MQService(IHubContext<MQServiceHub> hubContext,
                         ICapPublisher publisher)
        {
            this.hubContext = hubContext;
            this.publisher = publisher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SendToAllClient<TData>(TData data, Identity Identity = null, string ip = null) where TData : MQDataBase
        {
            MQData notifyData = new MQData();
            notifyData.Data = System.Text.Json.JsonSerializer.Serialize(data);
            notifyData.Identity = Identity;
            notifyData.Ip = ip;
            await SendToAllClient<MQData>(notifyData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SendToAllClient<TData>(TData notifyData) where TData : MQDataBase
        {
            await hubContext.Clients.All.SendAsync(Methods.MQSendAsync, notifyData);
        }

        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        public async Task SendToAllClient(MQData notifyData)
        {
            await SendToAllClient<MQData>(notifyData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public async Task SendToAllWithMQ<TData>(string topic, TData data, Identity Identity = null, string ip = null, string callBack = null) where TData: MQDataBase
        {
            MQData notifyData = new MQData();
            notifyData.Data = System.Text.Json.JsonSerializer.Serialize(data);
            notifyData.Identity = Identity;
            notifyData.Ip = ip;
            await SendToAllClient<MQData>(notifyData);
            await publisher.PublishAsync(topic, notifyData, callBack);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public async Task SendToAllWithMQ(string topic, MQData data, string callBack = null)
        {
            await SendToAllClient(data);
            await publisher.PublishAsync(topic, data, callBack);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="userId"></param>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SendToUser<TData>(int userId, TData data, Identity Identity = null, string ip = null) where TData : MQDataBase
        {
            MQData notifyData = new MQData();
            notifyData.Data = System.Text.Json.JsonSerializer.Serialize(data);
            notifyData.Identity = Identity;
            await SendToUser<MQData>(userId, notifyData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SendToUser(int userId, MQData notifyData)
        {
            await SendToUser<MQData>(userId, notifyData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="userId"></param>
        /// <param name="topic"></param>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <param name="callBack"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async Task SendToUserWithMQ<TData>(int userId, string topic,TData data, Identity Identity = null, string ip = null, string callBack = null) where TData : MQDataBase
        {
            MQData notifyData = new MQData();
            notifyData.Data = System.Text.Json.JsonSerializer.Serialize(data);
            notifyData.Identity = Identity;
            await SendToUser<MQData>(userId, notifyData);
            await publisher.PublishAsync(topic, notifyData, callBack);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public async Task SendWithMQ<TData>(string topic, TData data,string callBack = null)
        {
            await publisher.PublishAsync(topic, data, callBack);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        public async Task SendToUserWithMQ(int userId, string topic, MQData data, string callBack = null)
        {
            await SendToUser(userId, data);
            await publisher.PublishAsync(topic, data, callBack);
        }

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="userId"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        private async Task SendToUser<TData>(int userId, MQData notifyData) where TData : MQDataBase
        {
            await hubContext.Clients.User(userId.ToString()).SendAsync(Methods.MQSendAsync, notifyData);
        }
    }
}
