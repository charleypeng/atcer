// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion;
using Furion.RemoteRequest.Extensions;
using Furion.TaskScheduler;
using ATCer.EventBus;
using ATCer.NotificationSystem.Dtos.Notification;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATCer.SysTimer.Impl.Demo
{
    /// <summary>
    /// 测试定时任务
    /// </summary>
    /// <remarks>定时抓取财经新闻，作为聊天数据推送到客户端</remarks>
    public class DomeWorker : ISpareTimeWorker
    {
        private static long lastNewsId =0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="count"></param>
        [SpareTime(100,workerName: "测试定时任务")]
        public void DoSomething(SpareTimer timer, long count) 
        {
            ILogger logger= App.GetRequiredService<ILogger<DomeWorker>>();
            try
            {
                List<NewsInfo> resultNews = GetLastNews().Result;
                if (resultNews == null) { return; }
                foreach (var newsInfo in resultNews)
                {
                    if (newsInfo == null)
                    {
                        return;
                    }
                    IEventBus eventBus = App.GetRequiredService<IEventBus>();
                    ChatDemoNotificationData chatNotification = new ChatDemoNotificationData();
                    chatNotification.Avatar = "./assets/logo.png";
                    chatNotification.NickName = "系统";
                    chatNotification.Message = $"{newsInfo.digest}";
                    eventBus.Publish(chatNotification);
                }
            }
            catch (Exception ex) 
            {
                logger.LogError("测试定时任务执行异常", ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<List<NewsInfo>> GetLastNews()
        {
            Random random = new Random();
            string api = $"https://newsapi.eastmoney.com/kuaixun/v1/getlist_102__10_1_.html?r={random.Next()}&_={DateTime.Now.Millisecond}";

            var response =await api.GetAsync();
            string json = await response.Content.ReadAsStringAsync();
            NewsResult result= System.Text.Json.JsonSerializer.Deserialize<NewsResult>(json);

            if (result != null && result.LivesList != null && result.LivesList.Count>0) 
            {
                List<NewsInfo> resultNews = new List<NewsInfo>();
                foreach (var newsInfo in result.LivesList)
                {
                    if (newsInfo.newsidL > lastNewsId)
                    {
                        resultNews.Add(newsInfo);
                    }
                }
                if (resultNews.Count > 0) 
                {
                    lastNewsId=resultNews[0].newsidL;
                }

                return resultNews.OrderBy(x=>x.newsidL).ToList();
            }

            return null;

        }
    }

    public class NewsResult
    { 
    
        public List<NewsInfo> LivesList { get; set; }


    }

    public class NewsInfo
    {
        public string newsid { get; set; }
        public long newsidL { get { return long.Parse(newsid); } }
        public string url_w { get; set; }
        public string title { get; set; }
        public string digest { get; set; }
        public string showtime { get; set; }

    }
}
