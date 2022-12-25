// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATCer.LTFATCenter.Dtos.GZFips;
namespace ATCer.LTFATCenter.Services
{
    /// <summary>
    /// 用于FIPS的服务接口
    /// </summary>
    public interface IFipsService
    {
        /// <summary>
        /// 按日期获取航班信息
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="local"></param>
        /// <returns></returns>
        Task<IEnumerable<GZFlightPlanDto>> GetAsync(DateTime begin, DateTime end, string local="");
        /// <summary>
        /// 按ID获取航班信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Task<GZFlightPlanDto> GetAsync(int id);
        /// <summary>
        /// 获取当日计划
        /// </summary>
        /// <param name="local"></param>
        /// <returns></returns>
        Task<IEnumerable<GZFlightPlanDto>> GetToday(string local);
    }
}
