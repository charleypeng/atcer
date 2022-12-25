// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.UserCenter.Dtos;
using ATCer.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATCer.SystemManager.Dtos;

namespace ATCer.UserCenter.Services
{
    /// <summary>
    /// 角色服务接口
    /// </summary>
    public interface IRoleService : IServiceBase<RoleDto,int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<bool> DeleteResource( int roleId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="resourceIds"></param>
        /// <returns></returns>
        Task<bool> Resource(int roleId, Guid[] resourceIds);
        
        /// <summary>
        /// 获取角色所有资源
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetResource(int roleId);

        /// <summary>
        /// 获取种子数据
        /// </summary>
        /// <remarks>
        /// 获取种子数据
        /// </remarks>
        /// <returns></returns>
        Task<string> GetRoleResourceSeedData();
    }
}