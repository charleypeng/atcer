// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.ElasticSearch
{
    /// <summary>
    /// 自定义Elasticsearch类
    /// </summary>
    public class ATCerEsClient:ElasticClient, IATCerEsClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public ATCerEsClient(Uri uri) : this(new ConnectionSettings(uri)) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionSettings"></param>
        public ATCerEsClient(ConnectionSettings connectionSettings):base(connectionSettings) { }
    }
}
