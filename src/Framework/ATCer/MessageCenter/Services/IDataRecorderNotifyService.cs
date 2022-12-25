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

namespace ATCer.MessageCenter.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataRecorderNotifyService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="topic"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        Task Send(object message, string topic, string group = null);
    }
}
