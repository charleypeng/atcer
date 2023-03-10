using ATCer.Base.Domains;
using ATCer.Base.Enums;
using System;
using System.Collections.Generic;
namespace ATCer.HRCenter.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ResourceSeedData : IEntitySeedData<Resource>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {
                new Resource(){Id=Guid.Parse("D528C496-E3F4-467C-B3F8-0C3166767E90"),ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Name="精细化管理",Icon="clock-circle",Remark="精细化管理",Key="hrcenter",Path="/atcer/htcenter",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=ResourceType.Menu,Order=80},
                new Resource(){Id=Guid.Parse("D528C496-E3F4-467C-B3F8-0C3166767E91"),ParentId=Guid.Parse("D528C496-E3F4-467C-B3F8-0C3166767E90"),Name="扇区配置",Icon="robot",Remark="管理扇区",Key="hrcenter_sector_admin",Path="/atcer/hrcenter/sector-admin",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=ResourceType.Menu,Order=80},
                new Resource(){Id=Guid.Parse("D528C496-E3F4-467C-B3F8-0C3123767E91"),ParentId=Guid.Parse("D528C496-E3F4-467C-B3F8-0C3166767E90"),Name="导入小时数据",Icon="robot",Remark="导入小时数据",Key="hrcenter_timeitem_admin",Path="/atcer/hrcenter/import",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=ResourceType.Menu,Order=80}
            };
        }
    }
}