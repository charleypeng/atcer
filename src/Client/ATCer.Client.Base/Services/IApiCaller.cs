// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.Client.Base
{
    /// <summary>
    /// Restful Api 调用器
    /// </summary>
    public interface IApiCaller
    {
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        Task DeleteAsync(string url, IDictionary<string, object> queryString = null);
        /// <summary>
        /// 删除操作(带回调)
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        Task<TResponse> DeleteAsync<TResponse>(string url, IDictionary<string, object> queryString = null);
        /// <summary>
        /// 获取操作
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<TResponse> GetAsync<TResponse>(string url);
        /// <summary>
        /// 获取操作
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        Task<TResponse> GetAsync<TResponse>(string url, IDictionary<string, object> queryString);
        /// <summary>
        /// 获取操作
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        Task<TResponse> GetAsync<TResponse>(string url, List<KeyValuePair<string, object>> queryString);
        /// <summary>
        /// 推送操作
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request = default);
        /// <summary>
        /// 推送操作
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task PostAsync<TRequest>(string url, TRequest request = default);
        /// <summary>
        /// 补丁操作
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest request = default);
        /// <summary>
        /// 补丁操作
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task PutAsync<TRequest>(string url, TRequest request = default);
    }
}