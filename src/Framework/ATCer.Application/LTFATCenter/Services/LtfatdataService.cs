// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊 
//  CopyRight 2021  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.ElasticSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ATCer.LTFATCenter.Services
{
    /// <summary>
    /// 数据接收接口
    /// </summary>
    [ApiDescriptionSettings("LTFATServices")]
    public class LtfatDataService : BaseElasticService2<CAT048, string>,ILTFATDataService, IScoped
    {
        private readonly ILogger<LtfatDataService> _logger;

        /// <summary>
        /// 初始化
        /// </summary>
        public LtfatDataService(ILogger<LtfatDataService> logger,
                                IElasticClient elasticClient,
                                ICache cache) : base(elasticClient, cache)
        {
            _logger = logger;
        }

        /// <summary>
        /// Test: get cat048
        /// </summary>
        /// <remarks>
        /// Test: get cat048
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<CAT048>> GetCat048Async()
        {
            var cat = new CAT048();
            cat.VelocityInPolar = new VelocityInPolar { GroundSpeed_kt = 23123, Heading_degree = 2313 };
            cat.ModeSMBData = new ModeSMBData
            {
                BDS = null,
                BDS30 = "#21",
                BDS40 = new BDS40 { BarometricPressureSet_mb = 23, FMSSelectAltitude_m = 321, MCPorFCUSelectAltitude_m = 231 },
                BDS50 = new BDS50 { GroundSpeed = 32, RollAngle = 231, TrackAngleRate = 231, TrueTrackAngle = 321 },
                BDS60 = new BDS60 { BarometricAltitudeRate = 342, IndicatedAirspeed = 12 }

            };
            cat.CallSign = "rewe1";


            var cat2 = new CAT048();
            cat2.VelocityInPolar = new VelocityInPolar { GroundSpeed_kt = 23123, Heading_degree = 2313 };
            cat2.ModeSMBData = new ModeSMBData
            {
                BDS = null,
                BDS30 = "#21",
                BDS40 = new BDS40 { BarometricPressureSet_mb = 23, FMSSelectAltitude_m = 321, MCPorFCUSelectAltitude_m = 231 },
                BDS50 = new BDS50 { GroundSpeed = 32, RollAngle = 231, TrackAngleRate = 231, TrueTrackAngle = 321 },
                BDS60 = new BDS60 { BarometricAltitudeRate = 342, IndicatedAirspeed = 12 }

            };
            cat2.CallSign = "rewe1";

            var lst = new List<CAT048>();
            lst.Add(cat);
            lst.Add(cat2);

            return lst;
        }

        /// <summary>
        /// 存储Cat048数据
        /// </summary>
        /// <remarks>
        /// 存储Cat048数据
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> SaveCat048Async (Reciever<CAT048> entity)
        {
            if(entity == null)
            {
                return new BadRequestObjectResult("error model");
            }
            entity.Id = Guid.NewGuid().ToString();

            await _esCache.SetAsync(entity.Id, entity);

            var result = await _elasticClient.BulkAsync(b => b.Index(IndexName).IndexMany(entity.data));
            if (result.IsValid)
            {
                var ids = result.Items.Select(x => x.Id).ToList();
                var data =  System.Text.Json.JsonSerializer.Serialize(ids);
                return new OkObjectResult($"success, the stored Id is:{data}");
            }
            else
            {
                return new BadRequestObjectResult(result.OriginalException.Message);
            }
        }

        /// <summary>
        /// 存储数据测试
        /// </summary>
        /// <remarks>
        /// 存储数据测试
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> SaveDataTestAsync(Reciever<CAT048> entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            await _esCache.SetAsync(entity.Id, entity);
            
            return new OkObjectResult($"success, the stored Id is:{entity.Id}");
            
        }

        /// <summary>
        /// 获取cat048
        /// </summary>
        /// <remarks>
        /// 获取cat048
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CAT048> GetCat048Async(string id)
        {
            var result = await _elasticClient.GetAsync<CAT048>(id);
            if (result != null)
            {
                return (result.Source);
            }
            else
            {
                return null;
            }
        }

        [NonAction]
        [CapSubscribe("cat048test", Group = Group.DefaultGroup)]
        public async Task CAT048Insert(CAT048 data)
        {
            await SaveAsync(data);
        }
    }
}
