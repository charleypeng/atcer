// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.LTFATCenter.Domains.Elastic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using ATCer.Cache;
using Furion;
using Microsoft.Extensions.Logging;
using Furion.FriendlyException;
using ATCer.Base;
using Microsoft.AspNetCore.Authorization;

namespace ATCer.LTFATCenter.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ESFlightPlanService:ElasticSearch.Services.BaseElasticService<ESFlightPlan, Dtos.FlightPlanDto, long>
    {
        private readonly ILogger<ESFlightPlanService> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elasticClient"></param>
        /// <param name="cache"></param>
        /// <param name="logger"></param>
        public ESFlightPlanService(IElasticClient elasticClient,
                                   ICache cache,
                                   ILogger<ESFlightPlanService> logger) :base(elasticClient, cache, logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 分页添加 减少内存消耗
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task SyncNow(int pageIndex = 1, int pageSize = 100000)
        {
            var entities = Db.GetRepository<Domains.FlightPlan>().AsQueryable(false);
           
            var totalCount = await entities.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (totalPages == 0)
                return;

            _logger.LogInformation($"准备复制更新数据库，总共{totalPages}页");

            for (int i = 1; i <= totalPages; i++)
            {
                var items = await entities.Skip((i - 1) * pageSize).Take(pageSize).ToListAsync();
                await this.SaveManyAsync(items.Adapt<IList<ESFlightPlan>>().ToArray());
                _logger.LogInformation($"第{i}页复制完成");
            }

            _logger.LogInformation($"数据库更新完成");
        }

        public  async Task SaveBulkAsync(IEnumerable<ESFlightPlan> entities)
        {
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

        public  async Task SaveManyAsync(IEnumerable<ESFlightPlan> entities)
        {
            var result = await _elasticClient.IndexManyAsync(entities);
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
        private async Task<ATCer.Base.MyPagedList<FlightPlan>> GetPage(int pageIndex = 1, int pageSize = 10000)
        {
            var pageResult = Db.GetRepository<Domains.FlightPlan>().AsQueryable();

            var result = await pageResult.ToPageAsync(pageIndex, pageSize);

            return result;
        }

        public async Task<object> IWantU()
        {
            var de = new SearchDescriptor<ESFlightPlan>();
            
            var data = await _elasticClient.SearchAsync<ESFlightPlan>(sd=>sd
                .Size(10)
                .Query(q=>q
                    .Match(x=>x
                        .Field("remarks")
                        .Query("你好我的哎"))));
            
            return data.Documents;
        }

        public async Task ImportFromPostgreSql()
        {

        }
    }
}
