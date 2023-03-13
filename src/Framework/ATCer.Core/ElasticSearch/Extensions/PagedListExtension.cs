// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using Nest;
using ATCer.ElasticSearch;
using ATCer.Base;

namespace ATCer.Core.ElasticSearch.Extensions
{
    /// <summary>
    /// 分页扩展
    /// </summary>
    public static class PagedListExtension
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="client"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<MyPagedList<TEntity>> GetPagedList<TEntity>(this IElasticClient client, 
            int pageIndex = 1, 
            int pageSize = 10, 
            CancellationToken cancellationToken = default) 
            where TEntity : class, new()
        {
            if (pageIndex > 10000)
                throw Oops.Oh("不能大于10000");

            if (client == null) throw new ArgumentNullException("es client is null");

            var response = await client.CountAsync<TEntity>();
            var totalPages = (int)Math.Ceiling(response.Count / (double)pageSize);        
            var searchRequest = new SearchRequest
            {
                From = (pageIndex - 1) * pageSize,
                Size = pageSize,
            };
            var response2 = await client.SearchAsync<TEntity>(searchRequest,cancellationToken);

            var pageList = new MyPagedList<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = response2.Documents,
                TotalCount = (int)response.Count,
                TotalPages = totalPages,
                HasNextPages = pageIndex < totalPages,
                HasPrevPages = pageIndex - 1 > 0
            };
            return pageList;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="client"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchAfter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<MyPagedList<TEntity>> GetPagedList<TEntity>(this IElasticClient client,
            ESSearchAfter searchAfter,
            int pageIndex = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
            where TEntity : class, new()
        {
            if (client == null) throw new ArgumentNullException("es client is null");

            var response = await client.CountAsync<TEntity>(ct:cancellationToken);
            var totalPages = (int)Math.Ceiling(response.Count / (double)pageSize);
            var searchRequest = new SearchRequest
            {
                From = (pageIndex - 1) * pageSize,
                Size = pageSize,
                Sort = null
            };
            var response2 = await client.SearchAsync<TEntity>(searchRequest, cancellationToken);

            if(response2.IsValid)
            {
                var pageList = new MyPagedList<TEntity>()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Items = response2.Documents,
                    TotalCount = (int)response.Count,
                    TotalPages = totalPages,
                    HasNextPages = pageIndex < totalPages,
                    HasPrevPages = pageIndex - 1 > 0
                };
                return pageList;
            }
            else
            {
                LogError(response2);
                return null!;
            }
            
        }

        /// <summary>
        /// Response错误日志记录
        /// </summary>
        /// <param name="response"></param>
        public static void LogError(this IResponse? response)
        {
            var logger = App.GetService<ILogger>();
            
            if (response == null)
                return;

            logger.LogError(response?.ServerError?.Error?.Reason);

            var type = response?.GetType();
            if (type != typeof(BulkResponse)) return;
            var bulkResponse = response as BulkResponse;
            if (bulkResponse == null) return;
            foreach (var itemError in bulkResponse.ItemsWithErrors)
            {
                logger.LogError($"es error:id={itemError.Id}, {itemError.Error.Reason}");
            }
        }

        /// <summary>
        /// Response错误日志记录
        /// </summary>
        /// <param name="response"></param>
        /// <param name="logger">指定logger</param>
        public static void LogError(this IResponse? response, ILogger logger)
        {
            if (response == null)
                return;

            logger.LogError(response?.ServerError?.Error?.Reason);

            var type = response?.GetType();
            if (type != typeof(BulkResponse)) return;
            var bulkResponse = response as BulkResponse;
            if (bulkResponse == null) return;
            foreach (var itemError in bulkResponse.ItemsWithErrors)
            {
                logger.LogError($"es error:id={itemError.Id}, {itemError.Error.Reason}");
            }
        }
    }
}
