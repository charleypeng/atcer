// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATCer.Services
{
    /// <summary>
    /// Sync Service
    /// </summary>
    public interface ISyncService
    {
        /// <summary>
        /// Start task
        /// </summary>
        /// <returns></returns>
        Task Start();
        /// <summary>
        /// Stop task
        /// </summary>
        /// <returns></returns>
        Task Stop();
        /// <summary>
        /// Sync task
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task SyncNow(int pageIndex = 1, int pageSize = 10000);
        /// <summary>
        /// event handler
        /// </summary>
        event EventHandler EntityChanged;
    }
}
