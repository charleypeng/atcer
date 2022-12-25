// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace ATCer.NotificationSystem.Enums
{
    /// <summary>
    /// 用户在线状态
    /// </summary>
    public enum UserOnlineStatus
    {
        /// <summary>
        /// 上线
        /// </summary>
        [Description("上线")]
        Online,
        /// <summary>
        /// 离线
        /// </summary>
        [Description("离线")]
        Offline
    }
}
