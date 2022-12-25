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
    public class EmailService : IEmailService
    {
        private readonly string controller = "email";
        private readonly IApiCaller apiCaller;
        public EmailService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<bool> Send(SendEmailInputDto input)
        {
            return await apiCaller.PostAsync<SendEmailInputDto, bool>($"{controller}/send", input);
        }
    }
}
