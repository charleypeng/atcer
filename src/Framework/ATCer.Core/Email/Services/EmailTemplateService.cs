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
    /// 邮件模板服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class EmailTemplateService : ServiceBase<EmailTemplate, EmailTemplateDto, Guid>, IEmailTemplateService
    {

        private readonly IEmailService _emailService;

        /// <summary>
        /// 邮件模板服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="emailService"></param>
        public EmailTemplateService(IRepository<EmailTemplate> repository, IEmailService emailService) : base(repository)
        {
            _emailService = emailService;
        }
    }
}
