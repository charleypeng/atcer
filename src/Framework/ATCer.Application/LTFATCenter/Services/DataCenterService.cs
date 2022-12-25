// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Common;
using ATCer.LTFATCenter.Domains;
using System.Threading.Tasks;
using ATCer.LTFATCenter.Dtos;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using ATCer.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATCer.LTFATCenter.Services;

namespace ATCer.LTFATCenter.Services
{
    /// <summary>
    /// Datacenter controller service
    /// </summary>
    [ApiDescriptionSettings("LTFATServices")]
    public class DataCenterService: IDynamicApiController, IDashboardService
    {
        private readonly IRepository<FlightPlan> _flightPlanRepo;
        private readonly IRepository<SADProcedure> _sADProcedureRepo;
        private readonly IRepository<FlightArrInfo> _flightArrInfoRepo;
        private readonly IRepository<FlightDepInfo> _flightDepInfoRepo;
        private readonly ICache _cache;
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="flightPlanRepo"></param>
        /// <param name="sADProcedureRepo"></param>
        /// <param name="flightArrInfoRepo"></param>
        /// <param name="flightDepInfoRepo"></param>
        /// <param name="cache"></param>
        public DataCenterService(IRepository<FlightPlan> flightPlanRepo,
                                 IRepository<SADProcedure> sADProcedureRepo,
                                 IRepository<FlightArrInfo> flightArrInfoRepo,
                                 IRepository<FlightDepInfo> flightDepInfoRepo,
                                 ICache cache)
        {
            _flightPlanRepo = flightPlanRepo;
            _sADProcedureRepo = sADProcedureRepo;
            _flightArrInfoRepo = flightArrInfoRepo;
            _flightDepInfoRepo = flightDepInfoRepo;
            _cache = cache;
        }

        /// <remarks>
        /// 按起止时间获取SID统计 
        /// </remarks>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<XYDataOutput>> GetSIDStats(DateTimeOffset begin, DateTimeOffset end)
        {
            var beginStr = begin.ToString("yy-MM-dd-HH-mm-ss");
            var endStr = end.ToString("yy-MM-dd-HH-mm-ss");
            // get the data from cache
            // with 5sec cache refresh time
            // todo: need to set local airport
            var cdata = await _cache.GetAsync(CacheSchems.SIDStatsFrefix + beginStr + endStr, 
                                              async () => { return await setSIDStatsAsync(begin, end); },
                                              TimeSpan.FromSeconds(5));
            return cdata;
        }

        /// <summary>
        /// 获取SID统计 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IList<object>> GetSIDStats()
        {
#if DEBUG
            var dt = await GetSIDStats(new DateTime(2020, 03, 1), new DateTime(2021, 09, 1));
            return dt.Adapt<IList<object>>().ToList();
#endif
#if RELEASE
            var dt = await GetSIDStats(new DateTime(2020, 03, 1), new DateTime(2021, 09, 1));
            return dt.Adapt<IList<object>>().ToList();
#endif
        }

        private async Task<IEnumerable<XYDataOutput>> setSIDStatsAsync(DateTimeOffset begin, DateTimeOffset end)
        {
            var fr = _flightPlanRepo.AsQueryable(false);
            var sr = _sADProcedureRepo.AsQueryable(false);
            var dr = _flightDepInfoRepo.AsQueryable(false);

            //get flight plan data with dep info
            var dt = from a in fr
                     join b in dr
                     on a.Id equals b.FlightPlanID
                     where a.IsDeleted == false & b.IsDeleted == false
                     where a.EOBT >= begin && b.EOBT <= end
                     select new { Id = a.Id, SID = b.SID, CallSign = a.Callsign };

            //get sid info from SADProcedure entity
            var dt2 = from a in dt
                      join b in sr
                      on a.SID equals b.Name
                      select new { SID = a.SID, Direction = b.Direction };

            //group and get count of the sids
            var dt3 = dt2.GroupBy(x => x.Direction).Select(g => new XYDataOutput { Name = g.Key.ToString(), Data = g.Count() });

            return await dt3.ToListAsync();
        }
    }
}
