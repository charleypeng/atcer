// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Base;
using ATCer.Email.Dtos;
using System;

namespace ATCer.Email.Services
{
    /// <summary>
    /// 邮件服务器配置服务
    /// </summary>
    public interface IEmailServerConfigService : IServiceBase<EmailServerConfigDto,Guid>
    {

    }
}
