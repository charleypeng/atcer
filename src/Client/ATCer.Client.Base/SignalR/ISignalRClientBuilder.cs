// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.Client.Base
{
    /// <summary>
    /// SignalR Client构建器
    /// </summary>
    public interface ISignalRClientBuilder
    {
        /// <summary>
        /// 获取一个新实例
        /// </summary>
        /// <returns></returns>
        ISignalRClientBuilder GetInstance();
        /// <summary>
        /// 设置URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        ISignalRClientBuilder SetUrl(string url);
        /// <summary>
        /// 设置客户端名称
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        ISignalRClientBuilder SetClientName(string clientName);
        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        ISignalRClientBuilder SetHeader(Dictionary<string, string> headers);
        /// <summary>
        /// 设置日志记录器
        /// </summary>
        /// <param name="clientLogger"></param>
        /// <returns></returns>
        ISignalRClientBuilder SetLogger(IClientLogger clientLogger);
        /// <summary>
        /// 设置token提供者
        /// </summary>
        /// <param name="accessTokenProvider"></param>
        /// <returns></returns>
        ISignalRClientBuilder SetAccessTokenProvider(Func<Task<string?>> accessTokenProvider);
        /// <summary>
        /// 设置是否重连
        /// </summary>
        /// <param name="enableAutomaticReconnect"></param>
        /// <returns></returns>
        ISignalRClientBuilder SetEnableAutomaticReconnect(bool enableAutomaticReconnect = true);
        /// <summary>
        /// 构建client
        /// </summary>
        /// <returns></returns>
        ISignalRClient Build();
    }
}
