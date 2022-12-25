// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.SysTimer.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.SysTimer.Services
{
    public interface ISysTimerService:Base.IServiceBase<SysTimerDto, int>
    {
        void AddTimerJob(SysTimerDto input);

        Task Start(string jobName);

        Task Stop(string jobName);

        void StartTimerJob();

        /// <remarks>
        /// 获取所有本地任务
        /// </remarks>
        /// <returns></returns>
        Task<IEnumerable<TaskMethodInfo>> GetLocalJobs();
    }
}
