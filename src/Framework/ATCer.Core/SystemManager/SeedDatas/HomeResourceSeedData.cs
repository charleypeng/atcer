﻿// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Authentication.Enums;
using ATCer.Base.Domains;
using ATCer.Base.Enums;
using Microsoft.EntityFrameworkCore;

namespace ATCer.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class HomeResourceSeedData : IEntitySeedData<Resource>//, IEntitySeedData<ResourceFunction>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new Resource() {Name="首页",Key="admin_home",Remark="",Path="/",Icon="home",Order=10,ParentId=Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreatorIdentityType=Enum.Parse<IdentityType>("Unknown"),CreatedTime=DateTimeOffset.Parse("2021-11-09 07:41:45"),Id=Guid.Parse("371b335b-29e5-4846-b6de-78c9cc691717"),},
         };
        }

        ///// <summary>
        ///// 种子数据
        ///// </summary>
        ///// <param name="dbContext"></param>
        ///// <param name="dbContextLocator"></param>
        ///// <returns></returns>
        //IEnumerable<ResourceFunction> IPrivateEntitySeedData<ResourceFunction>.HasData(DbContext dbContext, Type dbContextLocator)
        //{
        //    return new[]{
        //        new ResourceFunction() {ResourceId=Guid.Parse("371b335b-29e5-4846-b6de-78c9cc691717"),FunctionId=Guid.Parse("8be6d20e-686c-4259-8eeb-3ec2b18739c3"),CreatedTime=DateTimeOffset.Parse("2022-08-12 13:54:54"),},
        // };
        //}
    }

}
