// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using Nest;

namespace ATCer.ElasticSearch.Services
{
    /// <summary>
    /// Elasticsearch基础服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseElasticService<TEntity,TEntityDto,TKey> : IDynamicApiController, IServiceBase<TEntityDto, TKey> 
        where TEntity : BaseElasticEntity<TKey>, new()
        where TEntityDto:class, new()
    {
        /// <summary>
        /// 缓存
        /// </summary>
        protected readonly ICache _cache;
        /// <summary>
        /// Index名称
        /// </summary>
        public virtual string IndexName { get; set; }
        /// <summary>
        /// es客户端
        /// </summary>
        protected virtual IElasticClient _elasticClient { get; init; }
        /// <summary>
        /// 缓存前缀
        /// </summary>
        protected virtual string cacheScheme { get; init; }
        /// <summary>
        /// Logging
        /// </summary>
        protected readonly ILogger _logger;
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="elasticClient"></param>
        /// <param name="cache"></param>
        /// <param name="logger"></param>
        /// <param name="indexName">索引名称(默认为Type小写名)</param>
        /// <param name="tatentId">多租户Id</param>
        public BaseElasticService(IElasticClient elasticClient, 
                                  ICache cache,
                                  ILogger logger,
                                  string indexName = "",
                                  string tatentId = "")
        {
            _elasticClient = elasticClient;
            _cache = cache;
            _logger = logger;
            if (string.IsNullOrWhiteSpace(indexName))
            {
                //index must be lowercase
                IndexName = (typeof(TEntity)).Name.ToLower() + "_" + tatentId; //App.Configuration["elasticsearch:index"].ToLower();
            }
            else
            {
                IndexName = indexName.ToLower();
            }
            //add cache prefix
            cacheScheme = Common.CacheSchems.ESId + "_" + nameof(TEntity) + "_" + tatentId;
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
        /// 错误日志记录
        /// </summary>
        /// <param name="response"></param>
        protected virtual void LogError(IResponse response)
        {
            if (response == null)
                return;

            _logger.LogError(response.ServerError.Error.Reason);

            var type = response.GetType();
            if (type == typeof(BulkResponse))
            {
                var bulkResponse = (BulkResponse)response;
                foreach (var itemError in bulkResponse.ItemsWithErrors)
                {
                    _logger.LogError($"es error:id={itemError.Id}, {itemError.Error.Reason}");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> Delete(TKey id)
        {
            var response = await _elasticClient.DeleteAsync<TEntity>(id?.ToString());
            LogError(response);

            if(response.IsValid)
            {
                var exists = await _cache.ExistsAsync(id.ToCacheId());
                if(exists)
                {
                    await _cache.RemoveAsync(id.ToCacheId(cacheScheme));
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Deletes(TKey[] ids)
        {
            var response = await _elasticClient.DeleteManyAsync(ids.Select(x => new TEntity { Id = x }));
            LogError(response);
            return response.IsValid;
        }

        
        public Task<bool> FakeDelete(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FakeDeletes(TKey[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateSeedData(MyPageRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntityDto> Get(TKey id)
        {
            var docPath = new DocumentPath<TEntity>(nameof(id));
            var result = await _elasticClient.GetAsync<TEntity>(docPath);
            LogError(result);

            if (result != null)
            {
                return (result.Source.Adapt<TEntityDto>());
            }
            else
            {
                return null;
            }
        }

        public Task<List<TEntityDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntityDto>> GetAllUsable()
        {
            throw new NotImplementedException();
        }

        public Task<MyPagedList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<TEntityDto> Insert(TEntityDto input)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Lock(TKey id, bool islocked = true)
        {
            throw new NotImplementedException();
        }

        public Task<MyPagedList<TEntityDto>> Search(MyPageRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TEntityDto input)
        {
            throw new NotImplementedException();
        }

        public Task<string> Export(MyPageRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
