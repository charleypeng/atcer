// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using ATCer.Base.Domains;
using MyHttpMethod = ATCer.Enums.MyHttpMethod;

namespace ATCer.NotificationSystem.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class FunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="示例服务",Service="聊天示例服务",Summary="获取聊天历史记录",Key="2A74937190C8E652BF107434EFFD1C17",Path="/api/chat-demo/history",Method=Enum.Parse<MyHttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("8be6d20e-686c-4259-8eeb-3ec2b18739c3"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }
    }



}
