//// -----------------------------------------------------------------------------
//// ATCer 全平台综合性空中交通管理系统
////  作者：彭磊  
////  CopyRight(C) 2023  版权所有 
//// -----------------------------------------------------------------------------

//using ATCer.Application.LTFATCenter.Domains;
//using ATCer.Cache;
//using ATCer.ElasticSearch.Services;
//using ATCer.LTFATCenter.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Nest;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace ATCer.Application.LTFATCenter.Services
//{
//    /// <summary>
//    /// 数据接收接口
//    /// </summary>
//    [ApiDescriptionSettings("LTFATServices")]
//    public class DataTest1Service : BaseElasticService2<CATItem1, string>, IScoped, ICapSubscribe
//    {
//        private readonly ILogger<LtfatDataService> _logger;
//        private readonly ICapPublisher _publisher;
//        /// <summary>
//        /// 初始化
//        /// </summary>
//        public DataTest1Service(ILogger<LtfatDataService> logger,
//                                IElasticClient elasticClient,
//                                ICapPublisher publisher,
//                                ICache cache) : base(elasticClient, cache)
//        {
//            _logger = logger;
//            _publisher = publisher;
//        }

        
//        /// <summary>
//        /// 存储Cat048数据
//        /// </summary>
//        /// <remarks>
//        /// 存储Cat048数据
//        /// </remarks>
//        [HttpPost]
//        public async Task<IActionResult> SaveCatItem1Async(Reciever<CATItem1> entity)
//        {
//            if (entity == null)
//            {
//                return new BadRequestObjectResult("error model");
//            }
//            entity.Id = Guid.NewGuid().ToString();

//            await _esCache.SetAsync(entity.Id, entity);

//            var result = await _elasticClient.BulkAsync(b => b.Index(IndexName).IndexMany(entity.data));
//            if (result.IsValid)
//            {
//                var ids = result.Items.Select(x => x.Id).ToList();
//                var data = System.Text.Json.JsonSerializer.Serialize(ids);
//                return new OkObjectResult($"success, the stored Id is:{data}");
//            }
//            else
//            {
//                return new BadRequestObjectResult(result.OriginalException.Message);
//            }
//        }

//        /// <summary>
//        /// 存储数据测试
//        /// </summary>
//        /// <remarks>
//        /// 存储数据测试
//        /// </remarks>
//        [HttpPost]
//        public async Task<IActionResult> SaveDataTestAsync(Reciever<CAT048> entity)
//        {
//            entity.Id = Guid.NewGuid().ToString();
//            await _esCache.SetAsync(entity.Id, entity);

//            return new OkObjectResult($"success, the stored Id is:{entity.Id}");

//        }

//        /// <summary>
//        /// 获取cat048
//        /// </summary>
//        /// <remarks>
//        /// 获取cat048
//        /// </remarks>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public async Task<CAT048> GetCat048Async(string id)
//        {
//            var result = await _elasticClient.GetAsync<CAT048>(id);
//            if (result != null)
//            {
//                return (result.Source);
//            }
//            else
//            {
//                return null;
//            }
//        }

//        [NonAction]
//        [CapSubscribe(name:"cat048test",Group = "msgtest1")]
//        public async Task CAT048Insert(string data)
//        {
//            _logger.LogWarning($"收到消息：{DateTime.Now}");
//            try
//            {
//                var jsdata = JsonConvert.DeserializeObject<CATItem1>(data);
//                if(jsdata != null)
//                {
//                    jsdata.Id = Guid.NewGuid().ToString("N");
//                    await SaveAsync(jsdata);
//                }  
//            }
//            catch (Exception)
//            {
//                throw Oops.Oh($"错误数据：{data}");
//            }
//        }

//        [AllowAnonymous]
//        public async Task testmq()
//        {
//            await _publisher.PublishAsync("cat048test", DateTime.Now);
//        }
//    }
//}
