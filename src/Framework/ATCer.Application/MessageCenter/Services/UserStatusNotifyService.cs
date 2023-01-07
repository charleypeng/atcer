// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.MessageCenter.Domains;
using ATCer.MessageCenter.Dtos;
using ATCer.EntityFramwork.DbContexts;
using Microsoft.AspNetCore.Mvc;
using static ATCer.Common.MQTopics.MQCenter;
using ATCer.MessageQueue.Core;
using ATCer.MessageQueue.Dtos;

namespace ATCer.MessageCenter.Services
{
    /// <summary>
    /// 用户状态跟踪
    /// </summary>
    public class UserStatusNotifyService: ServiceBase<UserStatusNotify, UserStatusNotifyDto, long, ATCerAuditDbContextLocator>, 
                                          IUserStatusNotifyService,
                                          ICapSubscribe,
                                          IScoped
    {
        private readonly ILogger<UserStatusNotifyService> logger;
        private readonly IMQService mqService;
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        /// <param name="mqService"></param>
        public UserStatusNotifyService(IRepository<UserStatusNotify, ATCerAuditDbContextLocator> repository,
                                       ILogger<UserStatusNotifyService> logger,
                                       IMQService mqService):base(repository)
        {
            this.logger = logger;
            this.mqService = mqService;
        }

        /// <summary>
        /// 删除所有状态
        /// </summary>
        /// <returns></returns>
        public async Task ClearAllStatus()
        {
            //var quary = _repository.AsQueryable()
            //                       .Where(x => x.IsDeleted == false);
            //await quary.ForEachAsync(x=>x.IsDeleted = true);

            var result = await _repository.Where(x => x.IsDeleted == false)
                                          .ExecuteUpdateAsync(x => x.SetProperty(x => x.IsDeleted, x => true));
            //var result = await _repository.SaveNowAsync();
        }

        /// <summary>
        /// 用户状态更新
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [NonAction]
        [CapSubscribe(MQUserStatusChanged, Group = Group.DefaultGroup)]
        public async Task UpdateUserStatus(UserStatusNotifyDto dto)
        {
            if (dto == null)
                return;

            //get where the current client is logged in
            //and fake delete all the data
            var result = await _repository.Where(x => x.ClientId == dto.ClientId && x.IsDeleted == false)
                                          .ExecuteUpdateAsync(x => x.SetProperty(x => x.IsDeleted, x => true));

            //insert new user status
            var insertResult = await base.Insert(dto);
            if(insertResult == null)
            {
                throw Oops.Oh("错误，无法添加用户状态");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        [NonAction]
        [CapSubscribe(MQSendToAll, Group = Group.DefaultGroup)]
        public void SendToAllSubscriber(MQData obj)
        {
            logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [NonAction]
        [CapSubscribe(Topic.Dashboard.DataCenterUpdateDashboard, Group = Group.DefaultGroup)]
        public async Task NotifyClientUpdateDashboard(object obj)
        {
            await mqService.SendToAllClient(new MQData { MQTopic = Topic.Dashboard.DataCenterUpdateDashboard });
        }
    }
}
