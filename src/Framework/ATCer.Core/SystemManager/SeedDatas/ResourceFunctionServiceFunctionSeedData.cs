// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Base.Domains;
using Microsoft.EntityFrameworkCore;
using MyHttpMethod = ATCer.Enums.MyHttpMethod;

namespace ATCer.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ResourceFunctionServiceFunctionSeedData : IEntitySeedData<Function>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Function> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
            new Function()
 {Group="用户中心服务",Service="资源与接口关系服务",Summary="添加资源与接口关系",Key="43844F96A173330CECD6470FD62A8A76",Description="",Path="/api/resource-function",Method=Enum.Parse<MyHttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("c1e7fa06-b759-4bb0-9545-7265e3798d28"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function()
 {Group="用户中心服务",Service="资源与接口关系服务",Summary="获取种子数据",Key="DDE05A70BD80F948C9AEAFB9708090F3",Description="",Path="/api/resource-function/seed-data",Method=Enum.Parse<MyHttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("c56d6a82-abc8-4b17-bc28-27b1904116c9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function()
 {Group="用户中心服务",Service="资源与接口关系服务",Summary="删除资源与接口关系",Key="FE150D4F1EE3DDDE5BD78C718100A247",Description="",Path="/api/resource-function/{resourceid}/{functionid}",Method=Enum.Parse<MyHttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("ffef6a8e-3f80-4a39-97c6-5b2b81582830"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
            };
        }
    }

}
