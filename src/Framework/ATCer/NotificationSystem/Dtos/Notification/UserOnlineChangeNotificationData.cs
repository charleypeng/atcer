// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.NotificationSystem.Dtos;
using ATCer.NotificationSystem.Enums;

namespace ATCer.NotificationSystem
{
    /// <summary>
    /// 用户在线状态变化通知数据
    /// </summary>
    public class UserOnlineChangeNotificationData : NotificationData
    {
        /// <summary>
        /// 用户在线状态变化通知数据
        /// </summary>
        public UserOnlineChangeNotificationData() : base(NotificationDataType.UserOnlineChange)
        {
        }
        
        /// <summary>
        /// 在线状态
        /// </summary>
        public UserOnlineStatus OnlineStatus { get; set; }
        
    }
}
