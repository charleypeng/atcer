﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Enums;
using System;

namespace ATCer.Authorization.Dtos
{
    /// <summary>
    /// api终结点
    /// </summary>
    public class ApiEndpoint
    {
        
        /// <summary>
        /// 唯一键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// API路由地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 接口请求方法
        /// </summary>
        public MyHttpMethod Method { get; set; }

        /// <summary>
        /// 唯一id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 启用审计
        /// </summary>
        public bool EnableAudit { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 服务
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    }
}
