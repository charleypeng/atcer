// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Dtos;
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
    public interface IAppTokenServce : IServiceBase<AppTokenDto, string>
    {
        /// <summary>
        /// 根据客户端编号获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<FunctionDto>> GetFunctions(string id);
        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <param name="appToken"></param>
        /// <returns></returns>
        Task<Identity> GetIdentity(string appToken);
        /// <summary>
        /// 生成口令
        /// </summary>
        /// <returns></returns>
        Task<string> GenerateToken();
        /// <summary>
        /// 获取JWT
        /// </summary>
        /// <param name="appToken"></param>
        /// <returns></returns>
        Task<string> GetJwtToken(string appToken);
        /// <summary>
        /// 导入到缓存
        /// </summary>
        /// <returns></returns>
        Task ImportToCache();
    }
}
