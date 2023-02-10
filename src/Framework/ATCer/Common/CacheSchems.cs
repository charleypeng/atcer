// -----------------------------------------------------------------------------
//  ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.Common
{
    /// <summary>
    /// 缓存代码
    /// </summary>
    public struct CacheSchems
    {
        /// <summary>
        /// 今日计划
        /// </summary>
        public const string FlightsOfToday = nameof(FlightsOfToday);
        /// <summary>
        /// SID缓存前缀
        /// </summary>
        public const string SIDStatsFrefix  = "SIDSTATS_";
        /// <summary>
        /// Elasticsearch Id 前缀
        /// </summary>
        public const string ESId  = "ESID_";
    }
}
