// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Authorization.Dtos;
using ATCer.Enums;
using System.Threading.Tasks;

namespace ATCer.Authorization.Core
{
    /// <summary>
    /// 接口查询服务
    /// </summary>
    public interface IApiEndpointQueryService
    {
        /// <summary>
        /// 清除缓存key
        /// </summary>
        /// <param name="apiEndpoint"></param>
        /// <returns></returns>
        Task ClearApiEndpointCacheKey(ApiEndpoint apiEndpoint);
        /// <summary>
        /// 根据path,method获取功能点
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <param name="enableCache"></param>
        /// <returns></returns>
        Task<ApiEndpoint> Query(string path, MyHttpMethod method,bool enableCache=true);
        /// <summary>
        /// 根据key获取功能点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="enableCache"></param>
        /// <returns></returns>
        Task<ApiEndpoint> Query(string key, bool enableCache = true);
    }
}
