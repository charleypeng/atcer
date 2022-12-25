// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using ATCer.Base.Domains;
using ATCer.Enums;

namespace ATCer.UserCenter.SeedDatas
{
    /// <summary>
    /// 接口种子数据
    /// </summary>
    public class ClientFunctionServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="用户中心服务",Service="客户端与接口关系服务",Summary="删除客户端与接口关系",Key="1ADEBA08C209B9D06D9D6788FB0509E6",Path="/api/client-function/{clientid}/{functionid}",Method=Enum.Parse<MyHttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("4963631e-6343-469a-a189-10bfce6e3195"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="用户中心服务",Service="客户端与接口关系服务",Summary="添加客户端与接口关系",Key="6C3EB756645619B25BF1323C05E781D8",Path="/api/client-function",Method=Enum.Parse<MyHttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("6e8d08f8-ba2a-4697-8b69-ac5a5bb31bff"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }

    }
}