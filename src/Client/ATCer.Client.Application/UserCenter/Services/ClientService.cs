// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authorization.Dtos;
using ATCer.Client.Base;
using ATCer.SystemManager.Dtos;
using ATCer.UserCenter.Dtos;
using ATCer.UserCenter.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Client.Services
{
    [ScopedService]
    public class ClientService : ClientServiceBase<ClientDto, Guid>, IClientService
    {
        public ClientService(IApiCaller apiCaller) : base(apiCaller, "client")
        {
        }
        public async Task<List<FunctionDto>> GetFunctions(Guid id)
        {
            return await apiCaller.GetAsync<List<FunctionDto>>($"{controller}/{id}/functions");
        }

        public async Task<TokenOutput> Login(ClientLoginInput input)
        {
            var result = await apiCaller.PostAsync<ClientLoginInput, TokenOutput>($"{controller}/login", input);
            return result;
        }

        public async Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            return await apiCaller.PostAsync<RefreshTokenInput, TokenOutput>($"{controller}/refresh-token", input);
        }
    }
}
