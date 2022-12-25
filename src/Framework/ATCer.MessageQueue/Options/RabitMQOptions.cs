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

namespace ATCer.MessageQueue.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class RabitMQOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; } = "admin";
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; } = "admin";
        /// <summary>
        /// 
        /// </summary>
        public string HostName { get; set; } = "192.168.2.161";
        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; } = 5672;
    }
}
