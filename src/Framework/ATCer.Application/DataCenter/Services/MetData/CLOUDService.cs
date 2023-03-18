// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.DataCenter;
using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Domains.MH4016_9;
using ATCer.DataCenter.Dtos.MetDatDtos;
using ATCer.DataCenter.Services.MetData;
using ATCer.ElasticSearch;
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
/// 云底高数据服务
/// </summary>
public class CloudService : BaseElasticService<CLOUD, CloudDto, string>, ICloudService, ICapSubscribe, ITransient
{
    /// <summary>
    /// 
    /// </summary>
    public CloudService(ILogger<CloudService> logger,
                        IATCerEsClient client,
                        ICache cache
                        ) : base(client, cache, logger, IndexNames.MetData_CLOUD)
    {

    }
}
