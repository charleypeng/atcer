// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using ATCer.UserCenter.Services;
using ATCer.UserCenter.Dtos;
using ATCer.Authorization.Dtos;
using ATCer.Client.Base;
using ATCer.SystemManager.Dtos;
using ATCer.Base.Enums;

namespace ATCer.UserCenter.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class AccountService : IAccountService
    {
        private readonly static string controller = "account";
        private IApiCaller apiCaller;

        public AccountService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<UserDto> GetCurrentUser()
        {
            return await apiCaller.GetAsync<UserDto>($"{controller}/current-user");
        }

        public async Task<List<ResourceDto>> GetCurrentUserMenus(string rootKey = null)
        {
            IDictionary<string, object> queryString = new Dictionary<string, object>();
            queryString.Add(nameof(rootKey), rootKey);
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/current-user-menus", queryString);
        }

        public async Task<List<string>> GetCurrentUserResourceKeys(params ResourceType[] resourceTypes)
        {
            List<KeyValuePair<string, object>> paras = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                paras.Add(new KeyValuePair<string, object>("resourceTypes", resourceTypes[i]));
            }
            return await apiCaller.GetAsync<List<string>>($"{controller}/current-user-resource-keys", paras);
        }

        public async Task<List<ResourceDto>> GetCurrentUserResources(params ResourceType [] resourceTypes)
        {
            List<KeyValuePair<string, object>> paras = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < resourceTypes.Length; i++)
            {
                paras.Add(new KeyValuePair<string, object> ("resourceTypes" ,resourceTypes[i]));
            }
            return await apiCaller.GetAsync<List<ResourceDto>>($"{controller}/current-user-resources", paras);
        }

        public async Task<List<RoleDto>> GetCurrentUserRoles()
        {
            return await apiCaller.GetAsync<List<RoleDto>>($"{controller}/current-user-roles");
        }

        public async Task<TokenOutput> Login(LoginInput input)
        {
            var result = await apiCaller.PostAsync<LoginInput, TokenOutput>($"{controller}/login", input);
            return result;
        }

        public async Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            return await apiCaller.PostAsync<RefreshTokenInput, TokenOutput>($"{controller}/refresh-token", input);
        }

        public async Task<bool> RemoveCurrentUserRefreshToken()
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/current-user-refresh-token");
        }
    }
}
