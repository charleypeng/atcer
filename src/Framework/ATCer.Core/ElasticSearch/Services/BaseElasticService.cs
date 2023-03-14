// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.Core.ElasticSearch.Extensions;
using Furion.DatabaseAccessor;
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
        where TEntity : ATCElasticEntity<TKey>, new()
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
        protected virtual IElasticClient _elasticClient { get; set; }
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
                                  string indexName = null!,
                                  string tatentId = "ILogger")
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
        protected virtual void Builder(Action buildOption = null!)
        {
            var configuration = App.Configuration;
            var url = configuration["elasticsearch:url"];
            var userName = configuration["elasticsearch:username"];
            var passWord = configuration["elasticsearch:password"];
            var settings = new ConnectionSettings(new Uri(url))
                .ServerCertificateValidationCallback((obj, cert, chain, sslPolicyErrors) => true)
                .BasicAuthentication(userName, passWord)
                .DefaultIndex(IndexName);
            _elasticClient = new ElasticClient(settings);
        }
        /// <summary>
        /// 错误日志记录
        /// </summary>
        /// <param name="response"></param>
        protected virtual void LogError(IResponse? response)
        {
            if (response == null)
                return;
            
            _logger.LogError(response?.ServerError?.Error?.Reason);

            var type = response?.GetType();
            if (type == typeof(BulkResponse))
            {
                var bulkResponse = response as BulkResponse;
                if (bulkResponse != null)
                {
                    foreach (var itemError in bulkResponse.ItemsWithErrors)
                    {
                        _logger.LogError($"es error:id={itemError.Id}, {itemError.Error.Reason}");
                    }

                }
            }
        }
        /// <summary>
        /// 删除
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

        /// <summary>
        /// 删除多项
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> Deletes(TKey[] ids)
        {
            var response = await _elasticClient.DeleteManyAsync(ids.Select(x => new TEntity { Id = x }));

            //log error
            response.LogError(_logger);

            return response.IsValid;
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> FakeDelete(TKey id)
        {
            var docPath = new DocumentPath<TEntity>(new TEntity { Id = id });
            var response = await _elasticClient.UpdateAsync(docPath, u => u.Index(IndexName).Doc(new TEntity { IsDeleted = true }));

            //log error
            response.LogError(_logger);

            return response.IsValid;
        }

        /// <summary>
        /// 假删除多项
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> FakeDeletes(TKey[] ids)
        {
            if (ids.IsNullOrEmpty())
                throw Oops.Oh(ExceptionCode.VALUE_CANNOT_BE_NULL);

            var lst = from a in ids
                      select new
                      {
                          Id = a,
                          IsDeleted = true
                      };

            var response = _elasticClient.Bulk(b => b.Index(IndexName).UpdateMany(lst, (bu, d) => bu.Doc(d)));

            response.LogError(_logger);

            return Task.FromResult(response.IsValid);
        }

        public Task<string> GenerateSeedData(MyPageRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntityDto> Get(TKey id)
        {
            var docPath = new DocumentPath<object>(new {Id = id, IsDeleted = false});
            var response = await _elasticClient.GetAsync(docPath);

            response.LogError(_logger);

            return response?.Source.Adapt<TEntityDto>()!;
        }

        public Task<List<TEntityDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntityDto>> GetAllUsable()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MyPagedList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            var result = await _elasticClient.GetPagedList<TEntity>(pageIndex, pageSize);
            return result.Adapt<MyPagedList<TEntityDto>>();
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TEntityDto> Insert(TEntityDto input)
        {
            var entity = input.Adapt<TEntity>();
            var response = await _elasticClient.IndexDocumentAsync(entity);

            response.LogError(_logger);
            if(response.IsValid)
            {
                return input;
            }
            else
            {
                throw Oops.Oh("错误的存储");
            }
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="id"></param>
        /// <param name="islocked"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> Lock(TKey id, bool islocked = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询
        /// <para>目前无</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MyPagedList<TEntityDto>> Search(MyPageRequest request)
        {
            var result = await _elasticClient.GetPagedList<TEntity>(request.PageIndex, request.PageSize);
            return result.Adapt<MyPagedList<TEntityDto>>();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> Update(TEntityDto input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> Export(MyPageRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
