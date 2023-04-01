// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using ATCer.Cache;
using ATCer.DataCenter.Dtos.MetDatDtos;
using ATCer.DataCenter.Services.MetData;
using ATCer.ElasticSearch.Services;

namespace ATCer.Application.DataCenter.Services.MetData;

/// <summary>
/// 降水数据服务
/// </summary>
[ApiDescriptionSettings(Module = "datacenter/met", Groups = new [] { "DataCenter" })]
public class RainService : BaseElasticService<RAIN, RainDto, string>, IRainService, ITransient
{
    /// <summary>
    /// 
    /// </summary>
    public RainService(ILogger<RainService> logger,
                        IATCerEsClient client,
                        ICache cache
                        ) : base(client, cache, logger, IndexNames.MetData_RAIN)
    {

    }
}
