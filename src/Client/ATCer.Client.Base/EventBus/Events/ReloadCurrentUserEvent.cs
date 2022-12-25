﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authorization.Dtos;
using ATCer.EventBus;

namespace ATCer.Client.Base.EventBus.Events
{
    /// <summary>
    /// 重载当前用户事件
    /// </summary>
    public class ReloadCurrentUserEvent:EventBase
    {
        /// <summary>
        /// 登录token
        /// </summary>
        public TokenOutput Token { get; set; }
    }
}
