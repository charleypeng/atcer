// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.SystemManager.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.SystemManager.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResourceFunctionService
    {


        /// <summary>
        /// 添加资源与接口关系
        /// </summary>
        /// <param name="resourceFunctionDtos"></param>
        /// <returns></returns>
        Task<bool> Add(List<ResourceFunctionDto> resourceFunctionDtos);

        /// <summary>
        /// 删除资源与接口关系
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid resourceId, Guid functionId);

        /// <summary>
        /// 获取种子数据
        /// </summary>
        /// <remarks>
        /// 获取种子数据
        /// </remarks>
        /// <returns></returns>
        Task<string> GetSeedData(List<Guid> resourceIds);
    }
}
