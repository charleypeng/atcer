// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Dtos;
using ATCer.Base;
using System;

namespace ATCer.Authentication.Services
{
    /// <summary>
    /// 用户Token服务
    /// </summary>
    public interface ILoginTokenService : IServiceBase<LoginTokenDto, Guid>
    {
    }
}
