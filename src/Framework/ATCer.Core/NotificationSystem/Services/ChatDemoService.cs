// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DynamicApiController;
using ATCer.NotificationSystem.Dtos.Notification;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace ATCer.NotificationSystem.Services
{
    /// <summary>
    /// 聊天示例服务
    /// </summary>
    [ApiDescriptionSettings("Demos")]
    public class ChatDemoService : IChatDemoService, IDynamicApiController
    {
        private static ConcurrentQueue<ChatDemoNotificationData > concurrentQueue=new ConcurrentQueue<ChatDemoNotificationData >();
        private static object lockObj=new object();
        /// <summary>
        /// 获取聊天历史记录
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<IEnumerable<ChatDemoNotificationData >> GetHistory()
        {
            IEnumerable<ChatDemoNotificationData > datas= concurrentQueue.ToList();
            return Task.FromResult(datas);
        }
        /// <summary>
        /// 添加一条消息通知到临时队列
        /// </summary>
        /// <param name="data"></param>
        public static void AddChatMessage(ChatDemoNotificationData  data)
        {
            lock (lockObj)
            {
                if (concurrentQueue.Count >= 200)
                {
                    //队列里最多200条
                    concurrentQueue.TryDequeue(out _);
                }
                //入队
                concurrentQueue.Enqueue(data);
            }
            
        }
    }
}
