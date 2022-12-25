// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.UserCenter.Dtos;
using ATCer.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATCer.SystemManager.Dtos;

namespace ATCer.UserCenter.Services
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserService:IServiceBase<UserDto, int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetResources(int userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<RoleDto>> GetRoles(int userId);
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<bool> Role(int userId, int[] roleIds);

        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> UpdateAvatar(UserUpdateAvatarInput input);
    }
}