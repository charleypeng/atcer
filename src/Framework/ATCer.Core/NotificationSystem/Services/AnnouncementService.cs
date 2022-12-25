// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.NotificationSystem.Domains;
using ATCer.NotificationSystem.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ATCer.NotificationSystem.Services
{
    /// <summary>
    /// 公告服务
    /// </summary>
    [ApiDescriptionSettings("NotificationSystem")]
    public class AnnouncementService : ServiceBase<Announcement, AnnouncementDto>, IAnnouncementService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AnnouncementService(IRepository<Announcement> repository) : base(repository)
        {
        }
    }
}
