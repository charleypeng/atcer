// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.Common
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public struct MQTopics
    {
        /// <summary>
        /// MQ default
        /// </summary>
        public struct MQCenter
        {
            /// <summary>
            /// 发送给所有
            /// </summary>
            public const string MQSendToAll = "MQ.Send.To.All";
            /// <summary>
            /// 发送已点击
            /// </summary>
            public const string MQSendInvoke = "MQ.Send.Invoke";
            /// <summary>
            /// 发送给用户
            /// </summary>
            public const string MQSendToUser = "MQ.Send.To.User";
            /// <summary>
            /// 用户状态已改变
            /// </summary>
            public const string MQUserStatusChanged = "MQ.User.StatusChanged";
        }
        public struct Dashboard
        {
            public const string DataCenterUpdateDashboard = "DataCenter.Dashboard.Update";
            public const string DataCenterClearAll = "DataCenter.Clear.All";
            public const string DataCenterCheckStatus = "DataCenter.Check.Status";
        }
    }
}
