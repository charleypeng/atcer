// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
namespace ATCer.ElasticSearch
{
    /// <summary>
    /// Elastic Search Base Entity基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    [ElasticsearchType(IdProperty =nameof(TKey))]
    public abstract class BaseElasticEntity<TKey>:IBaseElasticEntity<TKey>,IEquatable<TKey>
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        [DisplayName("主键")]
        public virtual TKey Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TKey? other)
        {
            return EqualityComparer<TKey>.Default.Equals(this.Id, other);
        }
    }
}
