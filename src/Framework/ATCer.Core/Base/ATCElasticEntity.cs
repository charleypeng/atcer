// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.ElasticSearch;
using Nest;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ATCer
{
    /// <summary>
    /// Elastic Search Base Entity基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    [ElasticsearchType(IdProperty = nameof(TKey))]
    public abstract class ATCElasticEntity<TKey> : BaseElasticEntity<TKey>, IBaseElasticEntity<TKey>, IEquatable<TKey>
    {

        /// <summary>
        /// 假删除
        /// </summary>
        [DisplayName("假删除")]
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTimeOffset CreatedTime { get; set; }
    }
}
