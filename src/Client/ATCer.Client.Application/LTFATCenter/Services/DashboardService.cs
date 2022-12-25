// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.LTFATCenter.Services;

namespace ATCer.LTFATCenter.Client.Services
{
    [ScopedService]
    public class DashboardService : IDashboardService
    {
        private readonly IApiCaller apiCaller;
        private readonly string controller;

        public DashboardService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
            this.controller = "data-center";
        }

        public async Task<IList<object>> GetSIDStats()
        {
            return await apiCaller.GetAsync<IList<object>>($"{controller}/sid-stats");
        }
    }
}
