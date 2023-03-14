// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.DataCenter;
using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Domains.MH4016_9;
using ATCer.ElasticSearch.Interfaces;
using ATCer.ElasticSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.Application.DataCenter.Services.MetData;

/// <summary>
/// 
/// </summary>
public class CloudService : BaseElasticService<CLOUD, CLOUD, string>, IESIndex, ICapSubscribe, ITransient
{
    /// <summary>
    /// 
    /// </summary>
    public CloudService(ILogger<CloudService> logger,
                        IElasticClient client,
                        ICache cache
                        ) : base(client, cache, logger, IndexNames.MetData_CLOUD)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    [CapSubscribe("datacenter.met.raw.cloud", Group = "metdata")]
    [NonAction]
    public async Task PressWorker(RawMetData data)
    {
        var mdata = data.ToMetType<CLOUD>();
        if (mdata == null)
        {
            _logger.LogError(message: $"{DateTimeOffset.FromUnixTimeSeconds(data.TIME.Value)} 收到的原始数据有误");
        }

        mdata.Id = Guid.NewGuid().ToString("N");

        var result = await this.Insert(mdata);
        if (result != null)
            _logger.LogInformation($"cloud已入库：datetime{DateTime.Now}:{JsonConvert.SerializeObject(mdata)}");
    }
    /// <summary>
    /// 
    /// </summary>
    public Func<bool>? Mapping { get; set; }
}
