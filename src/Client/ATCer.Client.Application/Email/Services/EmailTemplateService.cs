// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Email.Dtos;
using ATCer.Email.Services;

namespace ATCer.Email.Client.Services
{
    [ScopedService]
    public class EmailTemplateService : ClientServiceBase<EmailTemplateDto,Guid>, IEmailTemplateService
    {
        public EmailTemplateService(IApiCaller apiCaller) : base(apiCaller, "email-template")
        {
        }
    }
}
