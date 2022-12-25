// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace ATCer.Cache
{
    public class CacheOptions : IConfigurableOptions
    {
        /// <summary>
        /// 使用缓存类型
        /// Memory 内存
        /// SqlServer SqlServer
        /// Redis Redis
        /// </summary>
        public string Type { get; set; } = "Memory";
        public SqlServerCacheOptions SqlServer { get; set; }
        public RedisCacheOptions Redis { get; set; }
        //public NCacheOptions NCache { get; set; }
    }

    public class SqlServerCacheOptions
    {
        public string ConnectionString { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
    }
    public class RedisCacheOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Configuration { get; set; }
        /// <summary>
        /// 键名前缀
        /// </summary>
        public string InstanceName { get; set; }
    }
    public class NCacheOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string CacheName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool EnableLogs { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public bool ExceptionsEnabled { get; set; } = true;
    }
}
