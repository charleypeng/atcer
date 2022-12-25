﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATCer.MessageQueue.Enums;
using Furion.DatabaseAccessor;
using ATCer.EntityFramwork.DbContexts;

namespace ATCer.MessageCenter.Domains
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public class UserStatusNotify: IEntity<MasterDbContextLocator, ATCerAuditDbContextLocator>
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public UserOnlineStatus OnlineStatus { get; set; }
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTimeOffset IssueTime { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// 假删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
