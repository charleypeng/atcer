// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.NotificationSystem.Enums
{
    /// <summary>
    /// 通知数据类型
    /// </summary>
    public enum NotificationDataType
    {
        /// <summary>
        /// 用户上线下线
        /// </summary>
        UserOnlineChange,
        /// <summary>
        /// 聊天
        /// </summary>
        Chat,
        /// <summary>
        /// 系统消息
        /// </summary>
        System,
        /// <summary>
        /// 网络信号
        /// </summary>
        Network,
        /// <summary>
        /// 心跳信号
        /// </summary>
        HeartBeat,
        /// <summary>
        /// 系统更新信息
        /// </summary>
        SysUpdate,
        /// <summary>
        /// 公告信息
        /// </summary>
        Announcement
    }
}
