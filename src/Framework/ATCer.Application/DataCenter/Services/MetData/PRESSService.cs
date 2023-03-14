// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.DataCenter.Domains;
using ATCer.ElasticSearch.Interfaces;
using ATCer.ElasticSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json;

namespace ATCer.DataCenter.Services.MetData;

/// <summary>
/// 
/// </summary>
public class PRESSService: BaseElasticService<PRESS,PRESS,string>,IESIndex,ICapSubscribe,IScoped
{
    /// <summary>
    /// 
    /// </summary>
    public PRESSService(ILogger<PRESSService> logger,
                        IElasticClient client,
                        ICache cache
                        ):base(client,cache,logger,"metdata.press")
    {

    }
    
    /// <summary>
    /// 
    /// </summary>
    [CapSubscribe("datacenter.met.raw.press",Group = "metdata")]
    [NonAction]
    public async Task PressWorker(RawMetData data)
    {
         var mdata = data.ToMetType<PRESS>();
         if (mdata == null)
         {
             _logger.LogError(message: $"{DateTimeOffset.FromUnixTimeSeconds(data.TIME.Value)} 收到的原始数据有误");
         }
         
         mdata.Id = Guid.NewGuid().ToString("N");
         
         var result = await this.Insert(mdata);
         if(result != null)
             _logger.LogInformation($"已入库：datetime{DateTime.Now}:{JsonConvert.SerializeObject(mdata)}");
    }
    /// <summary>
    /// 
    /// </summary>
    public Func<bool>? Mapping { get; set; }
}