// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.MessageQueue.Dtos;
using ATCer.MessageQueue.Enums;
using ATCer.Authentication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.MessageQueue.Core
{
    /// <summary>
    /// Message queue service interface
    /// </summary>
    public interface IMQService
    {
        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task SendToAllClient<TData>(TData data, Identity Identity = null, string ip = null) where TData : MQDataBase;
        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToAllClient(MQData notifyData);
        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="userId"></param>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task SendToUser<TData>(int userId, TData data, Identity Identity = null, string ip = null) where TData : MQDataBase;
        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToUser(int userId, MQData notifyData);
        /// <summary>
        /// 发送MQ信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        /// <param name="callBack"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task SendToAllWithMQ<TData>(string topic, TData data, Identity Identity = null, string ip = null, string callBack = null) where TData : MQDataBase;
        /// <summary>
        /// 发送MQ信息
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        Task SendToAllWithMQ(string topic, MQData data, string callBack = null);
        /// <summary>
        /// 向指定用户发送MQ信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="userId"></param>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        /// <param name="callBack"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task SendToUserWithMQ<TData>(int userId, string topic, TData data, Identity Identity = null, string ip = null, string callBack = null) where TData : MQDataBase;
        /// <summary>
        /// 向指定用户发送MQ信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="topic"></param>
        /// <param name="data"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
        Task SendToUserWithMQ(int userId, string topic, MQData data, string callBack = null);
        Task SendWithMQ<TData>(string topic, TData data, string callBack = null);
    }
}
