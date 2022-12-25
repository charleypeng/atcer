// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Dtos;

namespace ATCer.MessageQueue.Dtos
{
    /// <summary>
    /// MQ 数据模型
    /// </summary>
    public class MQData:MQDataBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MQTopic { get; set; }
        /// <summary>
        /// 发送者身份
        /// </summary>
        public Identity Identity { get; set; }
        /// <summary>
        /// 消息处理回调事件
        /// </summary>
        public string CallBack { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 用户ip
        /// </summary>
        public string Ip { get; set; }
    }

    /// <summary>
    /// 指定类的MQ数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MQData<T>:MQDataBase
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MQTopic { get; set; }
        /// <summary>
        /// 发送者身份
        /// </summary>
        public Identity Identity { get; set; }
        /// <summary>
        /// 消息处理回调事件
        /// </summary>
        public string CallBack { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 用户ip
        /// </summary>
        public string Ip { get; set; }
    }
}
