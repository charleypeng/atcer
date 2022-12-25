// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.NotificationSystem.Enums;

namespace ATCer.NotificationSystem.Dtos.Notification
{
    /// <summary>
    /// 聊天数据
    /// </summary>
    public class ChatDemoNotificationData : NotificationData
    {
        /// <summary>
        /// 聊天数据
        /// </summary>
        public ChatDemoNotificationData() : base(NotificationDataType.Chat)
        {
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 图片列表
        /// </summary>
        public string[] Images { get; set; }
    }
}
