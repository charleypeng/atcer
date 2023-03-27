// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.DataCenter.Domains;
using ATCer.ElasticSearch.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ATCer.Application.DataCenter.Services
{
    /// <summary>
    /// 数据接收接口
    /// </summary>
    public class MetDataService : BaseElasticService<MyMetData, MyMetData, string> ,ITransient, ICapSubscribe
    {
        private readonly ICapPublisher _publisher;
        /// <summary>
        /// 初始化
        /// </summary>
        public MetDataService(ILogger<MetDataService> logger,
                              ICapPublisher publisher,
                              IATCerEsClient esClient,
                              ICache cache):base(esClient,cache,logger, IndexNames.MetData_Raw)
        {
            _publisher = publisher;
        }


        /// <summary>
        /// MHT4016.9原始数据解码器
        /// </summary>
        /// <param name="data1"></param>
        /// <returns></returns>
        [CapSubscribe("data.raw.mh4029_3")]
        [NonAction]
        public async Task MHT4016_9Receiver(string data1)
        {
            if (string.IsNullOrWhiteSpace(data1))
                return;

            try
            {
                var data = JsonSerializer.Deserialize<RawMetData>(data1);

                if (data == null)
                    return;
                var pushmsg = WorkerNames.Met_Raw_Prefix + data?.TYPE?.ToLower();
                await _publisher.PublishAsync(pushmsg, data);

                var mdata = (MyMetData)data!;

                if (mdata == null)
                    return;

                mdata.Id = Guid.NewGuid().ToString("N");
            }
            catch (Exception)
            {
                throw Oops.Oh($"转换自观原始数据出错:{JsonSerializer.Serialize(data1)}");
            }
           
            //var result = await this.Insert(mdata);
            //if (result != null)
            //    _logger.LogInformation($"已入库：datetime{DateTime.Now}:{JsonConvert.SerializeObject(data)}");
        }

        //public async Task MHT4016_9Receiver(RawMetData data)
        //{
        //    if (data == null)
        //        return;

        //    try
        //    {
        //        var pushmsg = WorkerNames.Met_Raw_Prefix + data?.TYPE?.ToLower();
        //        await _publisher.PublishAsync(pushmsg, data);

        //        var mdata = (MyMetData)data!;

        //        if (mdata == null)
        //            return;

        //        mdata.Id = Guid.NewGuid().ToString("N");
        //    }
        //    catch (Exception)
        //    {
        //        throw Oops.Oh($"转换自观原始数据出错:{JsonSerializer.Serialize(data)}");
        //    }

        //    //var result = await this.Insert(mdata);
        //    //if (result != null)
        //    //    _logger.LogInformation($"已入库：datetime{DateTime.Now}:{JsonConvert.SerializeObject(data)}");
        //}


        public async Task TestMet()
        {
            var jj = new RawMetData();
            await _publisher.PublishAsync("data.raw.mh4029_3", Encoding.UTF8.GetBytes(JsonSerializer.Serialize(jj)));
        }
    }
}
