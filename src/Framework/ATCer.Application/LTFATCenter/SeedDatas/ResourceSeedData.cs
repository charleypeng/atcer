

using ATCer.Base.Domains;
using ATCer.Base.Enums;

namespace ATCer.LTFATCenter.SeedDatas
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
                new Resource(){Id=Guid.Parse("3d93eb77-2a72-4b4f-aa79-5da1fc794400"),ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Name="飞行计划",Icon="robot",Remark="飞行计划管理",Key="atcer_ltfatcenter",Path="/atcer/ltfatcenter/fligh-plan-admin",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1636443705),IsDeleted=false,IsLocked=false,Type=ResourceType.Menu,Order=90}
            };
        }
    }
}