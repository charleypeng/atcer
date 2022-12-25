// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authorization.Dtos;
using ATCer.Base.Enums;
using ATCer.SystemManager.Dtos;
using ATCer.UserCenter.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Services
{
    /// <summary>
    /// 用户账户认证授权服务
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 获取当前用户的角色
        /// </summary>
        /// <returns></returns>
        Task<List<RoleDto>> GetCurrentUserRoles();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TokenOutput> Login(LoginInput input);
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        Task<TokenOutput> RefreshToken(RefreshTokenInput input);
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        Task<UserDto> GetCurrentUser();
        /// <summary>
        /// 获取指定类型的资源
        /// </summary>
        /// <param name="resourceTypes"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetCurrentUserResources(params ResourceType[] resourceTypes);
        /// <summary>
        /// 获取用户资源的key
        /// </summary>
        /// <param name="resourceTypes">资源类型</param>
        /// <returns></returns>
        Task<List<string>> GetCurrentUserResourceKeys(params ResourceType[] resourceTypes);
        /// <summary>
        /// 获取当前用户的所有菜单
        /// </summary>
        /// <param name="rootKey"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetCurrentUserMenus(string rootKey = null);
        /// <summary>
        /// 移除当前用户token
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveCurrentUserRefreshToken();
    }
}
