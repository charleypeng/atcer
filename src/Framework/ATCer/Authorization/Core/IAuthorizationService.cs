// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authentication.Dtos;
using ATCer.Authorization.Dtos;
using System.Threading.Tasks;

namespace ATCer.Authorization.Core
{
    /// <summary>
    /// 授权服务
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        Identity GetIdentity();
        /// <summary>
        /// 获取当前请求的功能点
        /// </summary>
        /// <returns></returns>
        Task<ApiEndpoint> GetApiEndpoint();
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        Task<bool> ChecktContenxtApiEndpoint();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object GetIdentityId();
    }
}