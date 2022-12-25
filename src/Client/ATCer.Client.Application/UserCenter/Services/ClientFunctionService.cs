// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Client.Base;
using ATCer.UserCenter.Dtos;
using ATCer.UserCenter.Services;

namespace ATCer.UserCenter.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class ClientFunctionService : IClientFunctionService
    {
        private readonly static string controller = "client-function";
        private readonly IApiCaller apiCaller;
        public ClientFunctionService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }
        public async Task<bool> Add(List<ClientFunctionDto> clientFunctionDtos)
        {
            return await apiCaller.PostAsync<List<ClientFunctionDto>, bool>($"{controller}", clientFunctionDtos);
        }

        public async Task<bool> Delete(Guid clientId, Guid functionId)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{clientId}/{functionId}");
        }
    }
}
