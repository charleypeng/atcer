﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Authentication.Options
{
    /// <summary>
    /// Jwt 配置
    /// </summary>
    public class JWTSettingsOptions
    {
        /// <summary>
        /// 验证签发方密钥
        /// </summary>
        public bool? ValidateIssuerSigningKey { get; set; }

        /// <summary>
        /// 签发方密钥
        /// </summary>
        public string IssuerSigningKey { get; set; }

        /// <summary>
        /// 验证签发方
        /// </summary>
        public bool? ValidateIssuer { get; set; }

        /// <summary>
        /// 签发方
        /// </summary>
        public string ValidIssuer { get; set; }

        /// <summary>
        /// 验证签收方
        /// </summary>
        public bool? ValidateAudience { get; set; }

        /// <summary>
        /// 签收方
        /// </summary>
        public string ValidAudience { get; set; }

        /// <summary>
        /// 验证生存期
        /// </summary>
        public bool? ValidateLifetime { get; set; }

        /// <summary>
        /// 过期时间容错值，解决服务器端时间不同步问题（秒）
        /// </summary>
        public long? ClockSkew { get; set; }

        /// <summary>
        /// 过期时间（分钟）
        /// </summary>
        public long? ExpiredTime { get; set; }

        /// <summary>
        /// 加密算法
        /// </summary>
        public string Algorithm { get; set; }

        /// <summary>
        /// 获取或设置 RefreshToken有效期分钟数
        /// </summary>
        public double RefreshExpireMins { get; set; }

        /// <summary>
        /// 获取或设置 RefreshToken是否绝对过期,绝对过期的刷新token在{RefreshExpireMins}分钟后需要重新登录
        /// </summary>
        public bool IsRefreshAbsoluteExpired { get; set; } = true;
    }
}
