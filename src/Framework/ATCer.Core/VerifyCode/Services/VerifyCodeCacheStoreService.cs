// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.VerifyCode.Core;
using ATCer.VerifyCode.Enums;
using System;
using System.Threading.Tasks;

namespace ATCer.VerifyCode.CacheStore
{
    /// <summary>
    /// 图片验证码数据库存储服务
    /// </summary>
    public class VerifyCodeCacheStoreService : IVerifyCodeStoreService
    {
        private readonly ICache _cache;
        private readonly string keyPre = "ImageVerifyCode:";

        public VerifyCodeCacheStoreService(ICache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public async Task Add(VerifyCodeTypeEnum verifyCodeType, string key, string code, TimeSpan expire)
        {
            await _cache.SetAsync(keyPre+ verifyCodeType + key, code, expire);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetCode(VerifyCodeTypeEnum verifyCodeType, string key)
        {
            return await _cache.GetStringAsync(keyPre + verifyCodeType + key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task Remove(VerifyCodeTypeEnum verifyCodeType, string key)
        {
            await _cache.RemoveAsync(keyPre + verifyCodeType + key);
        }
    }
}
