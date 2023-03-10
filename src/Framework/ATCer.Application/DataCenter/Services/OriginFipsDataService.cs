// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.Common;
using ATCer.LTFATCenter.Domains.GZFips;
using ATCer.LTFATCenter.Dtos.GZFips;
using ATCer.LTFATCenter.Services;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace ATCer.Application.DataCenter.Services
{
    /// <summary>
    /// FIPS原始数据
    /// </summary>
    public class OriginFipsDataService : IDynamicApiController, IFipsService, IScoped
    {
        private readonly IRepository<GZFlightPlan, FipsDbContextLocator> _fipsRepo;
        private readonly ICache _cache;
        /// <summary>
        /// Init
        /// </summary>
        public OriginFipsDataService(IRepository<GZFlightPlan, FipsDbContextLocator> fipsRepo,
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
            if (local.IsNullOrEmpty())
                local = "ZGHA";
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
        public async Task<GZFlightPlanDto> GetAsync(int id)
        {
            var data = await _fipsRepo.FindAsync(id);
            if(data == null)
                return null!;
            try
            {
                var result = data.Adapt<GZFlightPlanDto>();
                return result;
            }
            catch (Exception ex)
            {
                throw Oops.Oh(ex.Message);
            }

        }

        /// <summary>
        /// 获取今日计划航班
        /// </summary>
        /// <remarks>
        /// 获取今日计划航班
        /// </remarks>
        [HttpGet]
        public async Task<IEnumerable<GZFlightPlanDto>> GetToday(string local = "")
        {
            if (string.IsNullOrWhiteSpace(local))
                local = "ZGHA";
            //get data from cache first
            var data = await _cache.GetAsync<IEnumerable<GZFlightPlanDto>>(CacheSchems.FlightsOfToday);
            if (!data.IsNullOrEmpty())
                return data;

            //get new data from time config
            var tcf = new TimeConfig();
            var result = await this.GetAsync(tcf.BeginTimeOfToday, tcf.EndTimeOfToday, local);

            return result;
        }
    }
}
