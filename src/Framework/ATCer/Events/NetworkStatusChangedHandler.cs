using System;

namespace ATCer
{
    /// <summary>
    /// 网络状态
    /// </summary>
    public enum NetworkStatus
    {
        /// <summary>
        /// 网络已断开
        /// </summary>
        Disconnected,
        /// <summary>
        /// 网络断开中
        /// </summary>
        Disconnecting,
        /// <summary>
        /// 网络已连接
        /// </summary>
        Connected,
        /// <summary>
        /// 网络连接中
        /// </summary>
        Connecting,
        /// <summary>
        /// 网络无法连接
        /// </summary>
        Unreachable,
        /// <summary>
        /// 网络严重错误
        /// </summary>
        FatalError,
    }
    /// <summary>
    /// 网络状态消息代理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="status"></param>
    public delegate void NetworkStatusChangedEventHandler(object sender, NetworkStatus status);
}