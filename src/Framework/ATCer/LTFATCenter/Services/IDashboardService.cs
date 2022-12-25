using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ATCer.LTFATCenter.Dtos;

namespace ATCer.LTFATCenter.Services
{
    /// <summary>
    /// 看板服务
    /// </summary>
    public interface IDashboardService
    {
        /// <summary>
        /// SID统计
        /// </summary>
        /// <returns></returns>
        Task<IList<object>> GetSIDStats();
    }
}