// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Dtos.MetDatDtos;
using ATCer.ElasticSearch.Interfaces;
using ATCer.ElasticSearch.Services;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json;

namespace ATCer.DataCenter.Services.MetData;

/// <summary>
/// 
/// </summary>
public class PressService: BaseElasticService<PRESS,PressDto,string>,IESIndex,ICapSubscribe,ITransient, IPressService
{
    /// <summary>
    /// 
    /// </summary>
    public PressService(ILogger<PressService> logger,
                        IElasticClient client,
                        ICache cache
                        ):base(client,cache,logger, IndexNames.MetData_PRESS)
    {

    }
    
    /// <summary>
    /// 
    /// </summary>
    [CapSubscribe("datacenter.met.raw.press",Group = "metdata")]
    [NonAction]
    public async Task PressWorker(RawMetData data)
    {
         var mdata = data.ToMetType<PressDto>();
         if (mdata == null)
         {
             _logger.LogError(message: $"{DateTimeOffset.FromUnixTimeSeconds(data.TIME.Value)} 收到的原始数据有误");
         }
         
         mdata.Id = Guid.NewGuid().ToString("N");
         
         var result = await this.Insert(mdata);
         if(result != null)
             _logger.LogInformation($"press已入库：datetime{DateTime.Now}:{JsonConvert.SerializeObject(mdata)}");
    }
    /// <summary>
    /// 
    /// </summary>
    public Func<bool>? Mapping { get; set; }
}