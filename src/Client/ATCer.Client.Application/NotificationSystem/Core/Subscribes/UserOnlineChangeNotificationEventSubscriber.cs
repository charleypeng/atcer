﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Enums;
using ATCer.Client.Base;
using ATCer.NotificationSystem.Enums;

namespace ATCer.NotificationSystem.Client.Core.Subscribes
{
    /// <summary>
    /// 
    /// </summary>
    [TransientService]
    public class UserOnlineChangeNotificationEventSubscriber : EventSubscriberBase<UserOnlineChangeNotificationData>
    {
        private readonly IClientNotifier clientNotifier;
        private readonly IAuthenticationStateManager authStateManager;

        public UserOnlineChangeNotificationEventSubscriber(IClientNotifier clientNotifier, IAuthenticationStateManager authStateManager)
        {
            this.clientNotifier = clientNotifier;
            this.authStateManager = authStateManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override async Task CallBack(UserOnlineChangeNotificationData e)
        {
            UserOnlineChangeNotificationData notificationData = e;
            //无数据 或 无身份 或 非user身份
            if (notificationData == null || notificationData.Identity==null || !notificationData.Identity.IdentityType.Equals(IdentityType.User))
            {
                return;
            }
            var user = await authStateManager.GetCurrentUser();
            //未登录，或是自己
            if (user==null || user.Id.ToString() == notificationData.Identity.Id) 
            {
                return;
            }
             
            if (notificationData.OnlineStatus.Equals(UserOnlineStatus.Online))
            { 
               await clientNotifier.Info("用户上线通知",$"{notificationData.Identity.GivenName} 刚刚上线了<br/>IP:[{notificationData.Ip}]");
            }else if (notificationData.OnlineStatus.Equals(UserOnlineStatus.Offline))
            {
               await clientNotifier.Info("用户离线通知", $"{notificationData.Identity.GivenName} 刚刚离线了<br/>IP:[{notificationData.Ip}]");
            }
        }
    }
}
