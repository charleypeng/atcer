// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.ElasticSearch
{
    /// <summary>
    /// Es扩展
    /// </summary>
    public static class IdExtension
    {
        /// <summary>
        /// Es的缓存Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="scheme"></param>
        /// <returns></returns>
        public static string ToCacheId<T>(this T id, string scheme = Common.CacheSchems.ESId)
        {
            return scheme + id.ToString();
        }
    }
}
