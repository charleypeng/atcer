// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.FriendlyException;
using Nest;
using Furion.DynamicApiController;
using Furion;
using ATCer.Cache;

namespace ATCer.ElasticSearch.Services
{
    /// <summary>
    /// Base ElasticSearch abstract class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseElasticService2<TEntity, TKey> : IDynamicApiController, IBaseElasticService<TEntity,TKey>
                                                              where TEntity:class, IBaseElasticEntity<TKey>
    {
        /// <summary>
        /// TODO: use redis
        /// </summary>
        protected List<TEntity> _cache = new List<TEntity>();
        /// <summary>
        /// 
        /// </summary>
        protected ICache _esCache;
        /// <summary>
        /// 
        /// </summary>
        public string IndexName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected virtual IElasticClient _elasticClient { get; init; }
        //public BaseElasticService(IElasticClient elasticClient, ICache esCache)
        //{
        //    _elasticClient = elasticClient;
        //    _esCache = esCache;
        //    IndexName = App.Configuration["elasticsearch:index"].ToLower();
        //    Builder();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elasticClient"></param>
        /// <param name="esCache"></param>
        /// <param name="indexName"></param>
        public BaseElasticService2(IElasticClient elasticClient, ICache esCache, string indexName = null)
        {
            _elasticClient = elasticClient;
            _esCache = esCache;
            if (string.IsNullOrWhiteSpace(indexName))
            {
                //index must be lowercase
                IndexName = App.Configuration["elasticsearch:index"].ToLower();
            }
            else
            {
                IndexName = indexName.ToLower();
            }
            Builder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildOption"></param>
        protected virtual void Builder(Action buildOption = null)
        {
            
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            var response = await _elasticClient.DeleteAsync<TEntity>(entity);

            if(response.IsValid)
            {
                if (_cache.Contains(entity))
                {
                    _cache.Remove(entity);
                }
            }
        }

        /// <summary>
        /// 按Id获取
        /// </summary>
        public virtual async Task<TEntity> GetAsync(string id)
        {
            var result = await _elasticClient.GetAsync<TEntity>(id);
            if (result != null)
            {
                return (result.Source);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取Index总数
        /// </summary>
        public virtual async Task<long> IndexCountAsync()
        {
            var response = await _elasticClient.CountAsync<TEntity>();
            if(response.IsValid)
            {
                return response.Count;
            }
            else
            {
                Oops.Bah(response.DebugInformation);
                return 0;
            }
        }

        /// <summary>
        /// Bulk存储
        /// </summary>
        /// <param name="entities"></param>
        public virtual async Task SaveBulkAsync(IEnumerable<TEntity> entities)
        {
            _cache.AddRange(entities);
            var result = await _elasticClient.BulkAsync(b => b.Index(IndexName).IndexMany(entities));
            if (result.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in result.ItemsWithErrors)
                {
                    Oops.Bah("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
            }
        }

        /// <summary>
        /// 存储多个
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task SaveManyAsync(IEnumerable<TEntity> entities)
        {
            _cache.AddRange(entities);
            var result = await _elasticClient.IndexManyAsync(entities, IndexName);
            if (result.Errors)
            {
                // the response can be inspected for errors
                foreach (var itemWithError in result.ItemsWithErrors)
                {
                    Oops.Bah("Failed to index document {0}: {1}",
                        itemWithError.Id, itemWithError.Error);
                }
            }
        }

        /// <summary>
        /// 存储一个实例
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task SaveAsync(TEntity entity)
        {
            _cache.Clear();

            if (_cache.Any(p => p.Id.Equals(entity.Id)))
            {
                await _elasticClient.UpdateAsync<TEntity>(entity, u => u.Doc(entity));
            }
            else
            {
                _cache.Add(entity);
                var result = await _elasticClient.IndexDocumentAsync(entity);

            }
        }
    }
}
