// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.SysTimer.Dtos;
using ATCer.SysTimer.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.SysTimer.Client.Services
{
    [ScopedService]
    public class SysTimerService : ClientServiceBase<SysTimerDto, int>, ISysTimerService
    {
        public SysTimerService(IApiCaller apiCaller) : base(apiCaller, "sys-timer")
        {

        }

        public void AddTimerJob(SysTimerDto input)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskMethodInfo>> GetLocalJobs()
        {
            return apiCaller.GetAsync<IEnumerable<TaskMethodInfo>>($"{controller}/local-jobs");
        }

        public Task Start(string jobName)
        {
            return apiCaller.PostAsync($"{controller}/start", jobName);
        }

        public void StartTimerJob()
        {
            throw new NotImplementedException();
        }

        public Task Stop(string jobName)
        {
            return apiCaller.PostAsync($"{controller}/stop", jobName);
        }
    }
}
