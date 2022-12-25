// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.NotificationSystem.Dtos;
using System.Threading.Tasks;

namespace ATCer.NotificationSystem.Core
{
    /// <summary>
    /// 系统通知服务
    /// </summary>
    public interface ISystemNotificationService
    {

        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToAllClient(NotificationData notifyData);

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToUser(int userId, NotificationData notifyData);
    }
}
