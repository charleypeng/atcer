// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authorization.Dtos;
using ATCer.Base;
using ATCer.SystemManager.Dtos;
using ATCer.UserCenter.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.UserCenter.Services
{
    /// <summary>
    /// 客户端服务
    /// </summary>
    public interface IClientService : IServiceBase<ClientDto, Guid>
    {
        /// <summary>
        /// 根据客户端编号获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<FunctionDto>> GetFunctions(Guid id);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TokenOutput> Login(ClientLoginInput input);

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        Task<TokenOutput> RefreshToken(RefreshTokenInput input);
    }
}
