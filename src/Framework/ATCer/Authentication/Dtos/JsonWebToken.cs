﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;

namespace ATCer.Authentication.Dtos
{
    /// <summary>
    /// JWT
    /// </summary>
    public class JsonWebToken
    {
        /// <summary>
        /// 获取或设置 用于业务身份认证的AccessToken
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 获取或设置 AccessToken有效期，UTC标准
        /// </summary>
        public long AccessTokenExpires { get; set; }
        /// <summary>
        /// 获取或设置 用于刷新AccessToken的RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 获取或设置 RefreshToken有效期，UTC标准
        /// </summary>
        public long RefreshTokenExpires { get; set; }

        /// <summary>
        /// 刷新Token是否过期
        /// </summary>
        public bool IsRefreshTokenExpired()
        {
            return RefreshTokenExpires > DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 刷新Token是否过期
        /// </summary>
        public bool IsAccessTokenExpired()
        {
            return AccessTokenExpires > DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}
