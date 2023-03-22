// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.ElasticSearch.Services;
using Furion.DynamicApiController;
using ATCer.Cache;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Threading.Tasks;
using ATCer.FDE;
using ATCer.MessageQueue.Dtos;

namespace ATCer.LTFATCenter.Services
{
    public class Record:ElasticSearch.BaseElasticEntity<string>
    {
        public string Cat { get; set; }
        public string Content { get; set; }
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;
    }

    public class MQRecord:MQData<string>
    { 
    }

    public class Wala
    {
        public string MyProperty { get; set; }
    }

    public interface IDataRecord
    {
        Task SaveAsync(Record entity);
    }

    //public class DataRecord: BaseElasticService2<Record, string>, IDataRecord, ISingleton, ICapSubscribe, IDynamicApiController
    //{
    //    public DataRecord(IElasticClient elasticClient,
    //                            ICache cache):base(elasticClient, cache)
    //    {

    //    }

    //    public async Task<object> GetMH40293(string id)
    //    {
    //        var data = await GetAsync(id);
    //        if(data.Cat == "4029.3")
    //        {
    //            var model = FDEConverter.Deserialize<IFPL>(data.Content);  //MessageConvert.DeserializeObject<IFPL>(data.Content);
    //            return model;
    //        }
    //        return null;
    //    }
    //    public override Task SaveAsync(Record entity)
    //    {
    //        entity.Id = Guid.NewGuid().ToString();
    //        return base.SaveAsync(entity);
    //    }

    //    /// <summary>
    //    /// 测试数据
    //    /// </summary>
    //    /// <param name="entity"></param>
    //    /// <returns></returns>
    //    [CapSubscribe(name: "SendMQ",Group ="atc1")]
    //    public async Task SaveDataTest(MQRecord entity)
    //    {
    //        var content = entity?.Data;
    //        if (content == null)
    //            return;

    //        var record = new Record() { Id = Guid.NewGuid().ToString(), Cat = "TESTDATA", Content = content};
    //        await base.SaveAsync(record);
    //    }
    //}
}
