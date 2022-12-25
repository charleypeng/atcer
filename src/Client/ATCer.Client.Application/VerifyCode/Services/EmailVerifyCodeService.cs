// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.VerifyCode.Dtos;
using ATCer.VerifyCode.Services;
using System.Threading.Tasks;

namespace ATCer.VerifyCode.Client.Services
{
    [ScopedService]
    public class EmailVerifyCodeService : IEmailVerifyCodeService
    {
        private readonly static string controller = "email-verify-code";
        private readonly IApiCaller apiCaller;
        public EmailVerifyCodeService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<EmailVerifyCodeOutput> Create(EmailVerifyCodeInput input)
        {
            return await apiCaller.PostAsync<VerifyCodeInput, EmailVerifyCodeOutput>($"{controller}", input);
        }

        public async Task<bool> Remove(string key)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{key}");
        }

        public async Task<bool> Verify(EmailVerifyCodeCheckInput verifyCodeInput)
        {
            return await apiCaller.PostAsync<EmailVerifyCodeCheckInput, bool>($"{controller}/verify", verifyCodeInput);
        }
    }
}
