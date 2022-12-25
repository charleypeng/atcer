// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------


using ATCer.Authentication.Dtos;
using ATCer.EventBus;
using ATCer.NotificationSystem.Enums;
using System;

namespace ATCer.NotificationSystem.Dtos
{
    /// <summary>
    /// 系统通知数据
    /// </summary>
    public class NotificationData : EventBase
    {
        /// <summary>
        /// 系统通知数据
        /// </summary>
        public NotificationData()
        {
            this.Time = DateTimeOffset.Now;
            this.TypeAssemblyName = this.GetType().AssemblyQualifiedName;
            this.EventType = EventType.SystemNotify;
        }

        /// <summary>
        /// 系统通知数据
        /// </summary>
        /// <param name="type">通知类型</param>
        public NotificationData(NotificationDataType type):this()
        {
            this.NotificationDataType = type;
        }

        /// <summary>
        /// 程序类型
        /// </summary>
        public String TypeAssemblyName { get; set; }

        /// <summary>
        /// 通知事件类型
        /// </summary>
        private NotificationDataType _notificationDataType;
        /// <summary>
        /// 通知事件类型
        /// </summary>
        public NotificationDataType NotificationDataType
        {
            get
            {
                return this._notificationDataType;
            }
            set
            {
                this._notificationDataType = value;
                this.EventGroup = value.ToString();
            }
        }

        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTimeOffset Time { get; set; }

        /// <summary>
        /// 发送者身份
        /// </summary>
        public Identity Identity { get; set; }

        /// <summary>
        /// 用户ip
        /// </summary>
        public string Ip { get; set; }
    }
}
