// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Email.Domains;
using ATCer.Email.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ATCer.Email.Services
{

    /// <summary>
    /// 邮件服务器配置服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class EmailServerConfigService : ServiceBase<EmailServerConfig, EmailServerConfigDto,Guid>, IEmailServerConfigService
    {
        /// <summary>
        /// 邮件服务器配置服务
        /// </summary>
        /// <param name="repository"></param>
        public EmailServerConfigService(IRepository<EmailServerConfig> repository) : base(repository)
        {
        }
    }
}
