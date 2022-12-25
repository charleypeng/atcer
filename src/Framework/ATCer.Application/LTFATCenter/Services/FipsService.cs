// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//
//  2021  版权所有 
// -----------------------------------------------------------------------------

using Furion.DynamicApiController;
using ATCer.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using ATCer.LTFATCenter.Domains.GZFips;
using ATCer.LTFATCenter.Dtos.GZFips;
using Furion.FriendlyException;
using Mapster;
using ATCer.Cache;

namespace ATCer.LTFATCenter.Services
{
    /// <summary>
    /// 管综系统FIPS交互服务
    /// </summary>
    [ApiDescriptionSettings("LTFATServices")]
    public class FipsService: IDynamicApiController, IFipsService
    {
        private readonly IRepository<GZFlightPlan,FipsDbContextLocator> _fipsRepo;
        private readonly ICache _cache;
        /// <summary>
        /// Init
        /// </summary>
        public FipsService(IRepository<GZFlightPlan,FipsDbContextLocator> fipsRepo,
                           ICache cache)
        {
            _fipsRepo = fipsRepo;
            _cache = cache;
        }

        /// <summary>
        /// 按日期获取航班信息
        /// </summary>
        /// <remarks>
        /// 按日期获取航班信息
        /// </remarks>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="local"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GZFlightPlanDto>> GetAsync(DateTime begin, DateTime end, string local = "")
        {
            IEnumerable<GZFlightPlan> data;
            data = string.IsNullOrEmpty(local) ?
                   await _fipsRepo.Where(x => x.EOBT >= begin && x.EOBT <= end).ToListAsync() :
                   await _fipsRepo.Where(x => x.EOBT >= begin && x.EOBT <= end)
                                  .Where(x => x.ADES == local || x.ADEP == local)
                                  .ToListAsync();

            return data.Adapt<IEnumerable<GZFlightPlanDto>>();
            
        }

        /// <summary>
        /// 按ID获取航班信息
        /// </summary>
        /// <remarks>
        /// 按ID获取航班信息
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Domains.FlightPlan> GetAsync(int id)
        {
            var data = await _fipsRepo.FindAsync(id);
            try
            {
                var result = data.Adapt<Domains.FlightPlan>();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        /// <summary>
        /// 获取今日计划航班
        /// </summary>
        /// <remarks>
        /// 获取今日计划航班
        /// </remarks>
        [HttpGet]
        public async Task<IEnumerable<GZFlightPlanDto>> GetToday(string local="")
        {
            //get data from cache first
            var data = await _cache.GetAsync<IEnumerable<GZFlightPlanDto>>(CacheSchems.FlightsOfToday);
            if (data != null)
                return data;

            //get new data from time config
            var tcf = new TimeConfig();
            var result = await this.GetAsync(tcf.BeginTimeOfToday, tcf.EndTimeOfToday, local);

            return result;
        }
    }
}
