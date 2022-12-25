// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Domains;
using ATCer.HRCenter.Dtos;
using ATCer.HRCenter.Enums;
using ATCer.HRCenter.Utils;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using ATCer.Cache;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoleDict = ATCer.HRCenter.Enums.ControllerConf;
namespace ATCer.HRCenter.Services
{
    /// <summary>
    /// 小时费计算服务
    /// </summary>
    [ApiDescriptionSettings("HRCenterServices")]
    public class CalculateTimeService: IDynamicApiController
    {
        private readonly ICache _cache;
        private readonly IRepository<Sector> _sectorRepo;
        private readonly IRepository<UserATCInfo> _userATCInfoRepo;
        private readonly IRepository<WorkTimeConf> _workTimeConfRepo;
        private readonly IRepository<TimeItem> _timeItemRepo;
        private readonly IRepository<CalTimeItem> _calTimeItemRepo;
        private WorkTimeConfDto currentWorkConf { get; set; }
        /// <summary>
        /// Init
        /// </summary>
        public CalculateTimeService(ICache cache,
                                    IRepository<Sector> sectorRepo,
                                    IRepository<UserATCInfo> userATCInfoRepo,
                                    IRepository<WorkTimeConf> workTimeConfRepo,
                                    IRepository<TimeItem> timeItemRepo,
                                    IRepository<CalTimeItem> calTimeItemRepo
                                    )
        {
            _cache = cache;
            _sectorRepo = sectorRepo;
            _userATCInfoRepo = userATCInfoRepo;
            _workTimeConfRepo = workTimeConfRepo;
            _timeItemRepo = timeItemRepo;
            _calTimeItemRepo = calTimeItemRepo;
        }

        /// <summary>
        /// 获取值班时间配置
        /// </summary>
        /// <returns></returns>
        public async Task<WorkTimeConfDto> GetWorkTimeConf()
        {
            var data = await _workTimeConfRepo.Where(x => x.IsDeleted == false && x.IsLocked == false).FirstOrDefaultAsync();
            if (data == null)
                throw Oops.Bah(ExceptionCode.WORK_TIME_CONF_NOT_SET);

            return data.Adapt<WorkTimeConfDto>();
        }

        /// <summary>
        /// 假删除所有时间状态
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async Task ClearAllWorkTimeConf()
        {
            var quary = _workTimeConfRepo.AsQueryable()
                                         .Where(x => x.IsDeleted == false);
            await quary.ForEachAsync(x => x.IsDeleted = true && x.IsLocked == true);
            var result = await _workTimeConfRepo.SaveNowAsync();
        }


        /// <summary>
        /// 测试计算
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CalTimeItemDto>> Calculate(DateTimeOffset begin, DateTimeOffset end)
        {
            var calQuery = await GetCalculateQuery(begin, end);
            var data =  await calQuery.ToListAsync();
            return data;
        }

        /// <summary>
        /// 获取计算Dto
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<IQueryable<CalTimeItemDto>> GetCalculateQuery(DateTimeOffset begin, DateTimeOffset end)
        {
            currentWorkConf = await GetWorkTimeConf();

            //var sectorQuery = _sectorRepo.AsDefaultQuaryable();
            //var userATCInfo = _userATCInfoRepo.AsDefaultQuaryable();
            //var workTimeConfQuery = _workTimeConfRepo.AsDefaultQuaryable();
            //var timeItemQuery = _timeItemRepo.AsDefaultQuaryable();
            //var calTimeItem = _calTimeItemRepo.AsDefaultQuaryable();
            var dict = RoleDict.RoleMultiplierDict;

            var query = from timeItem in _timeItemRepo.AsDefaultQuaryable()
                        where timeItem.EndTime >= begin && timeItem.EndTime <= end
                        
                        select new CalTimeItemDto
                        {
                            Id = timeItem.Id,
                            ControllerRole = timeItem.ControllerRole,
                            DayTimeSpan = WorkTimeUtil.WorkTimeCalculate(timeItem.BeginTime, timeItem.EndTime, currentWorkConf).Item1,
                            NightTimeSpan = WorkTimeUtil.WorkTimeCalculate(timeItem.BeginTime, timeItem.EndTime, currentWorkConf).Item2,
                            ControllerRoleMultiplier = dict[timeItem.ControllerRole]
                        };

            return query;
        }
    }
}
