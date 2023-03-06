// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.ElasticSearch
{
    /// <summary>
    /// Elasticsearch分页向量
    /// </summary>
    public class ESSearchAfter
    {
        /// <summary>
        /// 向量
        /// </summary>
        public long Direction { get; set; }
        /// <summary>
        /// UUID
        /// </summary>
        public string UUID { get; set; }
    }
}
