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
using ATCer.Base;
using ATCer.LTFATCenter.Dtos;
using ATCer.LTFATCenter.Domains;
using Furion.DatabaseAccessor;
using Microsoft.Extensions.Logging;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Furion;
using Microsoft.Extensions.Hosting;
using ATCer.Common;
using ATCer.Enums;
using MapsterMapper;
using ATCer.Cache;
using ATCer.MessageCenter.Dtos;
using ATCer.LTFATCenter.Domains.GZFips;
using Microsoft.EntityFrameworkCore;
using ATCer.Services;
using Furion.FriendlyException;
using Bogus;
using ATCer.LTFATCenter.Enums;
using ATCer;

namespace ATCer.LTFATCenter.Services
{
    /// <summary>
    /// 飞行计划服务
    /// </summary>
    [ApiDescriptionSettings("LTFATServices")]
    public class FlightPlanService : ServiceBase<FlightPlan, FlightPlanDto, long>, IFlightPlanService
    {
        private readonly ICache _cache;
        private readonly IDbRepository<FipsDbContextLocator> _fipsDbContext;
        private readonly ISyncService _syncService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cache"></param>
        /// <param name="syncService"></param>
        /// <param name="fipsDbContext"></param>
        public FlightPlanService(IRepository<FlightPlan> repository,
                                 ICache cache,
                                 ISyncService syncService,
                                 IDbRepository<FipsDbContextLocator> fipsDbContext) : base(repository)
        {
            _cache = cache;
            _fipsDbContext = fipsDbContext;
            _syncService = syncService;
        }

        /// <remarks>
        /// 更新当日计划
        /// </remarks>
        public async Task<bool> UpdatePlansOfToday()
        {
            var tcf = new TimeConfig();
            await UpdatePlansByDate(tcf.BeginTimeOfToday, tcf.EndTimeOfToday);
            return true;
        }

        public async Task<ATCer.Base.MyPagedList<FlightPlanDto>> Search(MyPageRequest request, int lastId, DateTimeOffset lastDate)
        {

            var pageSize = request.PageSize;
            var pageIndex = request.PageIndex;
            var queryable = GetReadableRepository().AsQueryable(false)
                                                   .OrderByDescending(x => x.CreatedTime)
                                                   .Where(x => x.IsDeleted == false)
                                                   .Where(x => x.DeleteFlag == false)
                                                   .Where(x=>x.CreatedTime < lastDate ||(x.CreatedTime == lastDate && x.Id < lastId))
                                                   .Skip(pageSize * (pageIndex - 1));

            return await queryable
                .Select(x => x.Adapt<FlightPlanDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }

        /// <summary>
        /// 按给定时间段更新计划
        /// </summary>
        /// <remarks>
        /// 按给定时间段更新计划
        /// </remarks>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePlansByDate(DateTime begin, DateTime end)
        {
            var f = _fipsDbContext.Change<GZFlightPlan>().AsQueryable();
            var farr = _fipsDbContext.Change<GZFlightArrInfo>().AsQueryable();
            var fdep = _fipsDbContext.Change<GZFlightDepInfo>().AsQueryable();

            var data = from a in f
                       join b in farr
                       on a.FlightPlanID equals b.FlightPlanID
                       select a;
            return true;
        }

        /// <summary>
        /// FIPS导入Now
        /// </summary>
        /// <remarks>
        /// FIPS导入
        /// </remarks>
        /// <returns></returns>
        public async Task SyncNowFips()
        {
            await _syncService.SyncNow();
            Oops.Oh("任务完成");
        }

        /// <summary>
        /// FIPS导入Now
        /// </summary>
        /// <remarks>
        /// FIPS导入
        /// </remarks>
        /// <returns></returns>
        public async Task StopSyncFips()
        {
            await _syncService.Stop();
            Oops.Oh("任务已停止");
        }

        ///// <summary>
        ///// FakeData
        ///// </summary>
        ///// <remarks>
        ///// FIPS导入
        ///// </remarks>
        ///// <returns></returns>
        //public async Task FakeData()
        //{
        //    var begin = new DateTime(1990, 1, 1);
        //    var end = new DateTime(2022, 10, 1);
        //    Randomizer.Seed = new Random(234432);
        //    var fake = new Faker();
        //    var date = fake.Date.Between(begin, end);

        //    var conf = new TimeConfig(date);
        //    //var d = dates.Generate();
        //    var flights = new Faker<FlightPlan>()
        //        .StrictMode(false)
        //        .UseSeed(3412)
        //        .RuleFor(x => x.FlightPlanID, f => f.Random.Int())
        //        .RuleFor(x => x.WakeTurbulence, f => f.PickRandom<WakeTurbulence>())
        //        .RuleFor(x => x.PlanStatus, f => f.PickRandom<PlanStatus>())
        //        .RuleFor(x => x.FlightState, f => f.PickRandom<FlightStatus>())
        //        .RuleFor(x => x.EOBT, f => f.Date.Between(conf.Yesterday, conf.Tomorrow))
        //        .RuleFor(x => x.ETOT, f => f.Date.Between(conf.Yesterday, conf.Tomorrow))
        //        .RuleFor(x => x.ATOT, f => f.Date.Between(conf.Yesterday, conf.Tomorrow))
        //        .RuleFor(x => x.SOBT, f => f.Date.Between(conf.Yesterday, conf.Tomorrow))
        //        .RuleFor(x => x., f => f.Date.Between(conf.Yesterday, conf.Tomorrow))
        //        .RuleFor(x => x.ETOT, f => f.Date.Between(conf.Yesterday, conf.Tomorrow))
        //        .RuleFor(x => x.ETOT, f => f.Date.Between(conf.Yesterday, conf.Tomorrow))
        //        .
        //}
    }
}
