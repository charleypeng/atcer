// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATCer.Base;
using ATCer.Client.Base;
using ATCer.Common;
using ATCer.SystemManager.Dtos;
using ATCer.UserCenter.Dtos;
using ATCer.UserCenter.Services;

namespace ATCer.UserCenter.Client.Services
{
    [ScopedService]
    public class UserService : ClientServiceBase<UserDto>,IUserService
    {
        public UserService(IApiCaller apiCaller):base(apiCaller, "user")
        {
        }

        public Task<List<ResourceDto>> GetResources(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoleDto>> GetRoles(int userId)
        {
            return await apiCaller.GetAsync<List<RoleDto>>($"{controller}/{userId}/roles");
        }
       
        public async Task<MyPagedList<UserDto>> Search(int[] deptIds, int pageIndex = 1, int pageSize = 10)
        {
            List<KeyValuePair<string, object>> pramas = deptIds.ConvertToQueryParameters("deptIds");

            return await apiCaller.GetAsync<MyPagedList<UserDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }

        public async Task<bool> Role(int userId, int[] roleIds)
        {
            return await apiCaller.PostAsync<int[],bool>($"{controller}/{userId}/role", roleIds);
        }

        public async Task<bool> UpdateAvatar(UserUpdateAvatarInput input)
        {
            return await apiCaller.PutAsync<UserUpdateAvatarInput, bool>($"{controller}/avatar", input);
        }
    }
}
