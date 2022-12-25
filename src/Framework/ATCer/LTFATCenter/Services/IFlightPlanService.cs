// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATCer.LTFATCenter.Dtos;
using ATCer;
using ATCer.Base;

namespace ATCer.LTFATCenter.Services
{
    /// <summary>
    /// 飞行计划服务
    /// </summary>
    public interface IFlightPlanService:IServiceBase<FlightPlanDto, long>
    {
        /// <summary>
        /// 更新当日计划
        /// </summary>
        Task<bool> UpdatePlansOfToday();
        /// <summary>
        /// 按给定时间段更新计划
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<bool> UpdatePlansByDate(DateTime begin, DateTime end);
    }
}
