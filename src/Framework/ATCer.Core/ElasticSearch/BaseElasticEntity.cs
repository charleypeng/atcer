// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using System.ComponentModel;
using Nest;

namespace ATCer.ElasticSearch
{
    /// <summary>
    /// Elastic Search Base Entity基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
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
        public virtual bool Equals(TKey? other)
        {
            return EqualityComparer<TKey>.Default.Equals(this.Id, other);
        }
    }
}
