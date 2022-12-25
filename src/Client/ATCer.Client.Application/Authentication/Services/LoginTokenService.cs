// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Dtos;
using ATCer.Authentication.Services;
using ATCer.Client.Base;
using System;
using System.Threading.Tasks;

namespace ATCer.Client.Services
{
    /// <summary>
    /// 用户Token服务
    /// </summary>
    [ScopedService]
    public class LoginTokenService : ClientServiceBase<LoginTokenDto, Guid>, ILoginTokenService
    {
        public LoginTokenService(IApiCaller apiCaller) : base(apiCaller, "login-token")
        {
        }

        public Task<bool> CheckLoginIdUsable(string clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Disable(Guid id, bool isDisabled = true)
        {
            return await apiCaller.PutAsync<object, bool>($"{controller}/{id}/disable/{isDisabled}");
        }
    }
}
