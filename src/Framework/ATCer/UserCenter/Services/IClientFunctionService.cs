// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.UserCenter.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClientFunctionService
    {
        /// <summary>
        /// 添加客户端与接口关系
        /// </summary>
        /// <param name="clientFunctionDtos"></param>
        /// <returns></returns>
        Task<bool> Add(List<ClientFunctionDto> clientFunctionDtos);

        /// <summary>
        /// 删除客户端与接口关系
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid clientId, Guid functionId);
    }
}
