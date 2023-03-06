// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.DataCenter.Domains;
using ATCer.ElasticSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json;

namespace ATCer.Application.DataCenter.Services
{
    /// <summary>
    /// 数据接收接口
    /// </summary>
    public class DataTest1Service : BaseElasticService<MetData, MetData, string>, IScoped, ICapSubscribe
    {
        private readonly ICapPublisher _publisher;
        /// <summary>
        /// 初始化
        /// </summary>
        public DataTest1Service(ILogger<DataTest1Service> logger,
                                IElasticClient elasticClient,
                                ICapPublisher publisher,
                                ICache cache) : base(elasticClient, cache, logger, "metdata")
        {
            _publisher = publisher;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        [CapSubscribe("MHT4016_9")]
        [NonAction]
        public async Task MHT4016_9(RawMetData data)
        {
            if (data == null)
                return;

            var mdata = (MetData)data;
            if (mdata == null)
                return;

            mdata.Id = Guid.NewGuid().ToString("N");
            mdata.CreatedTime= DateTimeOffset.Now;

            var result = await this.Insert(data);
            if(result != null)
                _logger.LogError($"已入库：datetime{DateTime.Now}:{JsonConvert.SerializeObject(data)}");
        }
    }
}
