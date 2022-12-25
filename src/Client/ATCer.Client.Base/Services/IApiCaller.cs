// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATCer.Client.Base
{
    public interface IApiCaller
    {
        Task DeleteAsync(string url, IDictionary<string, object> queryString = null);
        Task<TResponse> DeleteAsync<TResponse>(string url, IDictionary<string, object> queryString = null);
        Task<TResponse> GetAsync<TResponse>(string url);
        Task<TResponse> GetAsync<TResponse>(string url, IDictionary<string, object> queryString);
        Task<TResponse> GetAsync<TResponse>(string url, List<KeyValuePair<string, object>> queryString);
        Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request = default);
        Task PostAsync<TRequest>(string url, TRequest request = default);
        Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest request = default);
        Task PutAsync<TRequest>(string url, TRequest request = default);
    }
}