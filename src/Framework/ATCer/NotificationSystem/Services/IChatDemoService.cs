// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.NotificationSystem.Dtos.Notification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.NotificationSystem.Services
{
    /// <summary>
    /// 聊天示例服务
    /// </summary>
    public interface IChatDemoService
    {
        /// <summary>
        /// 获取历史聊天记录
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ChatDemoNotificationData >> GetHistory();
    }
}
