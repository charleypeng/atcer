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

namespace ATCer.VerifyCode.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class EmailVerifyCodeServiceFunctionSeedData : IEntitySeedData<Function>
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
                new Function() {Group="系统基础服务",Service="邮件验证码服务",Summary="移除验证码",Key="88BF07EAB2CA231DE36CF2C1A2D2546D",Path="/api/email-verify-code/{key}",Method=Enum.Parse<MyHttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("3896ea42-a5ed-4bc5-8dc5-21e0e5adb2fa"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件验证码服务",Summary="获取验证码",Key="E23CF3B8D86A5D0E1F13759117676687",Path="/api/email-verify-code",Method=Enum.Parse<MyHttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("ddeeea7e-09e3-42c1-b536-0ff16393db1c"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function() {Group="系统基础服务",Service="邮件验证码服务",Summary="验证验证码",Key="1113744E52468C0ED06582D699F77B87",Path="/api/email-verify-code/verify",Method=Enum.Parse<MyHttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("e466c648-4dc5-4ca4-b8f9-826c51b2a462"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),UpdatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
         };
        }
    }


}
