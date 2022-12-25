// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using ATCer.Authorization.Dtos;
using ATCer.Base.Domains;
using ATCer.Cache;
using ATCer.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ATCer.Authorization.Core
{
    /// <summary>
    /// 接口查询服务
    /// </summary>
    public class ApiEndpointQueryService : IApiEndpointQueryService
    {
        private readonly string cacheKeyPre = $"{nameof(ApiEndpoint)}:";
        private readonly ICache cache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        public ApiEndpointQueryService(ICache cache)
        {
            this.cache = cache;
        }
        /// <summary>
        /// 清除缓存key
        /// </summary>
        /// <param name="apiEndpoint"></param>
        /// <returns></returns>
        public async Task ClearApiEndpointCacheKey(ApiEndpoint apiEndpoint)
        {
            await cache.RemoveAsync(GetApiEndpointCacheKey(apiEndpoint.Path, apiEndpoint.Method));
            await cache.RemoveAsync(GetApiEndpointCacheKey(apiEndpoint.Key));
        }
        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetApiEndpointCacheKey(string key)
        { 
            return cacheKeyPre + key; 
        }
        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private string GetApiEndpointCacheKey(string path, MyHttpMethod method)
        { 
            return cacheKeyPre + path + "_" + method;
        }
        /// <summary>
        /// 根据key获取功能点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="enableCache"></param>
        /// <returns></returns>
        public async Task<ApiEndpoint> Query(string key, bool enableCache = true)
        {
            Func<Task<ApiEndpoint>> func = async () => {

                Function function = await Db.GetRepository<Function>().AsQueryable(false).Where(x => x.Key.Equals(key) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefaultAsync();
                return function?.Adapt<ApiEndpoint>();
            };
            if (!enableCache)
            {
                return await func();
            }
            string cacheKey = GetApiEndpointCacheKey(key);
            return await cache.GetAsync<ApiEndpoint>(cacheKey, func);
        }

        /// <summary>
        /// 根据path,method获取功能点
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <param name="enableCache"></param>
        /// <returns></returns>
        public async Task<ApiEndpoint> Query(string path, MyHttpMethod method, bool enableCache = true)
        {
            Func<Task<ApiEndpoint>> func = async () => {
                Function function = await Db.GetRepository<Function>().AsQueryable(false).Where(x => x.Method.Equals(method) && x.Path.Equals(path) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefaultAsync();
                return function?.Adapt<ApiEndpoint>();
            };
            if (!enableCache)
            {
                return await func();
            }
            string cacheKey = GetApiEndpointCacheKey(path, method);
            return await cache.GetAsync<ApiEndpoint>(cacheKey, func);
        }
    }
}
